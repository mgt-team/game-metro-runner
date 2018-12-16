using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ZonePoint {

    private readonly Vector2 _selfPosition;
    private GameObject _activeObject;

    public ZonePoint(Vector2 position)
    {
        _selfPosition = position;
        IsFree = true;
    }

    public bool IsFree { get; set; }

    public void AddElement(GameObject element)
    {
        _activeObject = element;
        IsFree = false;
    }

    public void DeleteElement()
    {
        if (!IsFree)
        {
            _activeObject = null;
            IsFree = true;
        }
    }

    public Vector2 GetPointPosition()
    {
        return _selfPosition;
    }
}
