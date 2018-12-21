using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    private readonly List<GameObject> _objects = new List<GameObject>();

    [SerializeField] private GameObject _objectPrefab;
    
    public GameObject GetObject(Vector2 position)
    {

        GameObject objectFromPool;
        if (_objects.Count == 0)
        {
             objectFromPool = InstantiateNewObject();
        }
        else
        {
            objectFromPool = _objects[_objects.Count - 1];
            _objects.Remove(objectFromPool);
        }
        
        objectFromPool.transform.position = position;
        return objectFromPool;
    }

    public void ReturnObject(GameObject returnedObject)
    {
        returnedObject.SetActive(false);
        returnedObject.transform.parent = transform;
        _objects.Add(returnedObject);
    }

    private GameObject InstantiateNewObject()
    {
        var newObject = Instantiate(_objectPrefab, gameObject.transform);
        newObject.SetActive(false);
        newObject.transform.parent = gameObject.transform;
        return newObject;
    }
}
