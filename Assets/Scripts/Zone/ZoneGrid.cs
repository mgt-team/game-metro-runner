using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGrid : MonoBehaviour {

    public float xMax;
    public float xMin;

    public float yMax;
    public float yMin;

    public ZoneGrid (Sprite background)
    {
        xMax = background.rect.xMax / 60;
        xMin = background.rect.xMin / 60;

        yMax = background.rect.yMax / 60;
        yMin = background.rect.yMin / 60;
    }

    public List<List<ZonePoint>> GetGrid(Vector2 zonePosition, int horizontalPointCount, int verticalPointCount)
    {
        List<List<ZonePoint>> countVZP = new List<List<ZonePoint>>();               //List of List verticalZonePoints
        float x = (xMax - xMin) / horizontalPointCount;
        float y = (yMax - yMin) / verticalPointCount;
        for (int i = 1; i < verticalPointCount; i++)
        {
            List<ZonePoint> verticalZonePoints = new List<ZonePoint>();
            for (int j = 1; j < horizontalPointCount; j++)
                verticalZonePoints.Add(new ZonePoint(new Vector2(x * j + (zonePosition.x - xMax / 2), y * i + (zonePosition.y - yMax / 2))));
            countVZP.Add(verticalZonePoints);
        }
        return countVZP;

    }

    public void GetVisualGrid(Vector2 backgroundPos, int horizontalPointCount, int verticalPointCount)
    {
        float x = (xMax - xMin) / horizontalPointCount;
        float y = (yMax - yMin) / verticalPointCount;
        for(int i = 1; i < horizontalPointCount; i++)
        {
            Vector2 end = new Vector2 (i * x - xMax/2 + backgroundPos.x, yMin - yMax / 2  + backgroundPos.y);                  //Этот метод строит сетку в редакторе
            Vector2 start = new Vector2(i * x - xMax/2 + backgroundPos.x, yMax - yMax / 2 + backgroundPos.y);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }

        for(int i = 1; i < verticalPointCount; i++)
        {
            Vector2 end = new Vector2(xMin - xMax / 2 + backgroundPos.x, y * i - yMax / 2 + backgroundPos.y);
            Vector2 start = new Vector2(xMax - xMax / 2 + backgroundPos.x, y * i - yMax / 2 + backgroundPos.y);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }
    }
}
