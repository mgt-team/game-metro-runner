using UnityEngine;

public class ZoneObjectsGenerator : Singleton<ZoneObjectsGenerator>
{
	[SerializeField]
	private float _coinGenerateOnRowProbability = 1;

	[SerializeField] private int _minCoinCount = 1;
	[SerializeField] private int _maxCoinCount = 1;

	private GameObject _coinsContainer;
	private GameObject _npcsContainer;

	private void Awake()
	{
		_coinsContainer = GameObject.FindGameObjectWithTag(
			TagManager.GetTagNameByEnum(TagEnum.CoinsContainer));
		_npcsContainer = GameObject.FindGameObjectWithTag(
			TagManager.GetTagNameByEnum(TagEnum.NPCsContainer));
	}

	public void GenerateOnGrid(ZoneGrid zoneGrid)
	{
		GenerateCoins(zoneGrid);
	}

	private void GenerateCoins(ZoneGrid zoneGrid)
	{
		// Choose direction
		var direction = Random.Range(-1, 1);
		
		for (var rowInd = 0; rowInd < zoneGrid.ZonePoints.Count; rowInd++)
		{
			// Skip if random says it
			if (Random.value > _coinGenerateOnRowProbability) continue;
			
			// Choose column index for position
			var columnInd = ChooseColumnIndForRowInGrid(zoneGrid, rowInd);
			
			// Generate sequence
			rowInd = GenerateCoinSequence(zoneGrid, rowInd, columnInd, direction);
		}
	}

	private int ChooseColumnIndForRowInGrid(ZoneGrid zoneGrid, int rowInd)
	{
		var freeColumns = zoneGrid.ZonePoints[rowInd].FindAll(column => column.IsFree);
		var randomColumn = freeColumns[Random.Range(0, freeColumns.Count - 1)];
		return zoneGrid.ZonePoints[rowInd].IndexOf(randomColumn);
	}

	private int GenerateCoinSequence(ZoneGrid zoneGrid, int startRow, int startColumn, int direction)
	{
		// Get random coins count
		var coinsCount = Random.Range(_minCoinCount, _maxCoinCount);
		
		// Generate coin sequence
		int currentRow = startRow, currentColumn = startColumn;
		for (;
			currentRow < coinsCount + startRow 
			&& currentRow < zoneGrid.ZonePoints.Count // Check if has row
			&& currentColumn >= 0 && currentColumn < zoneGrid.ColumnCount; // Check if has column
			currentRow++, currentColumn += direction) // Move to next point by direction
		{
			var currentZonePoint = zoneGrid.ZonePoints[currentRow][currentColumn];

			if (currentZonePoint.IsFree)
			{
				GenerateCoin(currentZonePoint);
			}
		}

		return currentRow - 1;
	}

	private void GenerateCoin(ZonePoint zonePoint)
	{
		// Get coin from pull
		var coinObject = PoolManager.Instance.CoinPool.GetObject(zonePoint.GetPointPosition());
		coinObject.SetActive(true);
		coinObject.transform.parent = _coinsContainer.transform;
		zonePoint.AddElement(coinObject);
	}
}
