using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGrid
{
    private readonly int _rowCount;
    private readonly int _columnCount;

    private readonly Vector2 _minCoordinate;
    private readonly Vector2 _maxCoordinate;
    
    private readonly float _rowDistanceBetweenPoints;
    private readonly float _columnDistanceBetweenPoints;

    private readonly List<List<ZonePoint>> _zonePoints;

    public ZoneGrid (Sprite background, Vector2 zonePosition, int rowCount, int columnCount)
    {
        _rowCount = rowCount;
        _columnCount = columnCount;
        
        _minCoordinate = new Vector2(background.rect.xMin / 60, background.rect.yMin / 60);
        _maxCoordinate = new Vector2(background.rect.xMax / 60, background.rect.yMax / 60);

        _rowDistanceBetweenPoints = (_maxCoordinate.x - _minCoordinate.x) / rowCount;
        _columnDistanceBetweenPoints = (_maxCoordinate.y - _minCoordinate.y) / columnCount;

        _zonePoints = GetGrid(zonePosition, rowCount, columnCount);
    }

    public List<List<ZonePoint>> ZonePoints
    {
        get { return _zonePoints; }
    }

    public int RowCount
    {
        get { return _rowCount; }
    }

    public int ColumnCount
    {
        get { return _columnCount; }
    }

    private List<List<ZonePoint>> GetGrid(Vector2 zonePosition, int rowCount, int columnCount)
    {
        List<List<ZonePoint>> zonePoints = new List<List<ZonePoint>>();               //List of List verticalZonePoints
        
        for (int i = 1; i < columnCount; i++)
        {
            List<ZonePoint> currentRowPoints = new List<ZonePoint>();
            for (int j = 1; j < rowCount; j++)
                currentRowPoints.Add(new ZonePoint(
                    new Vector2(_rowDistanceBetweenPoints * j + (zonePosition.x - _maxCoordinate.x / 2),
                        _columnDistanceBetweenPoints * i + (zonePosition.y - _maxCoordinate.y / 2))));
            zonePoints.Add(currentRowPoints);
        }
        return zonePoints;

    }

    /*
     * Visual grid in editor
     */ 
    public void GetVisualGrid(Vector2 backgroundPos, int rowCount, int columnCount)
    {
        var offsetVector = new Vector2(-_maxCoordinate.x / 2 + backgroundPos.x,
            -_maxCoordinate.y / 2 + backgroundPos.y);
        for(var i = 1; i < rowCount; i++)
        {
            var xCoordinate = i * _rowDistanceBetweenPoints + offsetVector.x;
            var end = new Vector2 (xCoordinate, _minCoordinate.y + offsetVector.y);
            var start = new Vector2(xCoordinate, _maxCoordinate.y + offsetVector.y);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }

        for(var i = 1; i < columnCount; i++)
        {
            var yCoordinate = _columnDistanceBetweenPoints * i + offsetVector.y;
            var end = new Vector2(_minCoordinate.x + offsetVector.x, yCoordinate);
            var start = new Vector2(_maxCoordinate.x + offsetVector.x, yCoordinate);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }
    }
}
