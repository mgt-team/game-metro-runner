using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePoint : MonoBehaviour{

    private Vector2 _selfPosition;
    private bool _isFree;
    private GameObject _activeObject;

    public ZonePoint(Vector2 position)
    {
        _selfPosition = position;
        _isFree = true;
    }

    public void AddElement(GameObject element)
    {
        _activeObject = Instantiate(element, _selfPosition, Quaternion.identity);
        _isFree = false;
    }

    public void DeleteElement()
    {
        if (!IsFree())
        {
            Destroy(_activeObject);
            _activeObject = new GameObject();
        }
    }

    public bool IsFree()
    {
        return _isFree;
    }
}
