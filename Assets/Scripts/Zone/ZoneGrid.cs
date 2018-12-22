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
        
        _minCoordinate = new Vector2(background.rect.xMin / background.pixelsPerUnit,
            background.rect.yMin / background.pixelsPerUnit);
        _maxCoordinate = new Vector2(background.rect.xMax / background.pixelsPerUnit,
            background.rect.yMax / background.pixelsPerUnit);

        _rowDistanceBetweenPoints = (_maxCoordinate.y - _minCoordinate.y) / rowCount;
        _columnDistanceBetweenPoints = (_maxCoordinate.x - _minCoordinate.x) / columnCount;

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
        var zonePoints = new List<List<ZonePoint>>();               //List of List verticalZonePoints
        
        for (var i = 1; i <= rowCount; i++)
        {
            var currentRowPoints = new List<ZonePoint>();
            for (var j = 1; j <= columnCount; j++)
                currentRowPoints.Add(new ZonePoint(
                    new Vector2(_columnDistanceBetweenPoints * j + (zonePosition.x - _maxCoordinate.x / 2),
                        _rowDistanceBetweenPoints * i + (zonePosition.y - _maxCoordinate.y / 2))));
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
        
        // Draw all columns
        for(var i = 1; i < columnCount; i++)
        {
            var xCoordinate = i * _columnDistanceBetweenPoints + offsetVector.x;
            var end = new Vector2 (xCoordinate, _minCoordinate.y + offsetVector.y);
            var start = new Vector2(xCoordinate, _maxCoordinate.y + offsetVector.y);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }

        // Draw all rows
        for(var i = 1; i < rowCount; i++)
        {
            var yCoordinate = _rowDistanceBetweenPoints * i + offsetVector.y;
            var end = new Vector2(_minCoordinate.x + offsetVector.x, yCoordinate);
            var start = new Vector2(_maxCoordinate.x + offsetVector.x, yCoordinate);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }
    }
}
