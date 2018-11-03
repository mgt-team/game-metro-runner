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

    public List<Vector2> GetGrid(Vector2 zonePosition)
    {
        List <Vector2> listPositions = new List<Vector2>();
        float x = (xMax - xMin) / 10;
        float y = (yMax - yMin) / 10;
        for (int i = 1; i < 10; i++)
            for (int j = 1; j < 10; j++)
                listPositions.Add(new Vector2(x * j +(zonePosition.x - xMax / 2), y * i + (zonePosition.y - yMax / 2)));
        return listPositions;

    }

    public void GetVisualGrid()
    {
        float x = (xMax - xMin) / 10;
        float y = (yMax - yMin) / 10;
        for(int i = 1; i < 10; i++)
        {
            Vector2 end = new Vector2 (i * x - xMax/2, yMin - yMax / 2);                  //Этот метод строит сетку в редакторе
            Vector2 start = new Vector2(i * x - xMax/2, yMax - yMax / 2);
            Debug.DrawLine(start, end, Color.yellow, .5f);
            end = new Vector2(xMin - xMax/2, y * i - yMax/2);
            start = new Vector2(xMax - xMax/2, y * i - yMax/2);
            Debug.DrawLine(start, end, Color.yellow, .5f);
        }
    }
}
