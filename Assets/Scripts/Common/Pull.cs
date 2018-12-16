using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour {
    private List<GameObject> _objects;

    [SerializeField] private int _objectsCount;
    [SerializeField] private GameObject _objectPrefab;

    public void Init()
    {
        for(int i = 0; i < _objectsCount; i++)
        {
            var newObject = Instantiate(_objectPrefab, gameObject.transform);
            newObject.SetActive(false);
            _objects.Add(newObject);
            newObject.transform.parent = gameObject.transform;
        }
    }

    
    public GameObject GetObject(Vector2 position)
    {
        var newObject = _objects[_objects.Count];
        newObject.transform.position = position;
        return newObject;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.parent = transform;
    }
}
