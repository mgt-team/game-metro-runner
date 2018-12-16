using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    private readonly List<GameObject> _objects = new List<GameObject>();

    [SerializeField] private GameObject _objectPrefab;
    
    public GameObject GetObject(Vector2 position)
    {
        if (_objects.Count == 0)
        {
            InstantiateNewObject();
        }
        
        var lastObject = _objects[_objects.Count - 1];
        _objects.Remove(lastObject);
        lastObject.transform.position = position;
        return lastObject;
    }

    public void ReturnObject(GameObject returnedObject)
    {
        returnedObject.SetActive(false);
        returnedObject.transform.parent = transform;
        _objects.Add(returnedObject);
    }

    private void InstantiateNewObject()
    {
        var newObject = Instantiate(_objectPrefab, gameObject.transform);
        newObject.SetActive(false);
        _objects.Add(newObject);
        newObject.transform.parent = gameObject.transform;
    }
}
