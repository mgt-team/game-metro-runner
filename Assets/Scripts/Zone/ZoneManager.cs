using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour {

    [SerializeField] 
    private List<Sprite> _backgrounds;
    [SerializeField] 
    private float _zonesDistance;
    [SerializeField] 
    private GameObject _zonesContainer;
    [SerializeField] 
    private Zone _zonePrefab;
    [SerializeField]
    private Zone _startZone;
    [SerializeField] 
    private int _zoneOnSceneCount;
    [SerializeField]
    private int _zoneCountOnStart;

    public Zone _currentZone;
    public Zone _newZone;
    
    private List<Zone> _zoneList;    

    private void Start()
    {
        for (var i = 0; i < _zoneCountOnStart; i++)
        {
            GenerateNextZone();
        }
    }

    public void GenerateNextZone()
    {
        if (_zoneList == null)
        {
            if (_startZone != null)
            {
                _zoneList = new List<Zone>();
                _zoneList.Add(_startZone);
                GenerateNextZone();
            }
            else
            {
                Debug.LogError("Generator doesn't have a start point");
            }
        }
        else
        {
            if (_zoneList.Count > _zoneOnSceneCount)                                //Необходимо добавить проверку на камеру
            {
                Destroy(_zoneList[0].gameObject);
                _zoneList.RemoveAt(0);
            }

            if (_zoneList.Count > 0)
            {
                InstantiateNextZone(_zonePrefab, _zoneList[_zoneList.Count - 1].transform.position);
                _newZone = _zoneList[_zoneList.Count - 1];
                _currentZone = _zoneList[_zoneList.Count - 2];
            } 
            else
            {
                Debug.LogError("Generator has empty zone list");
            }
        }
    }

    private void InstantiateNextZone(Zone nextZone, Vector2 previousZonePosition)
    {
        var zoneInstance = Instantiate(nextZone, previousZonePosition + Vector2.up * _zonesDistance, Quaternion.identity);
        zoneInstance.transform.parent = _zonesContainer.transform;
        _zoneList.Add(zoneInstance);
        zoneInstance.SetZoneProperties(GenerateProperties());
    }

    private ZoneProperties GenerateProperties()
    {
        ZoneProperties zoneProperties = new ZoneProperties();
        zoneProperties.background = _backgrounds[Random.Range(0, _backgrounds.Count - 1)];
        
        return zoneProperties;
    }
}
