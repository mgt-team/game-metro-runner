using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ZoneManager : MonoBehaviour {

    [SerializeField] 
    private List<Sprite> _backgrounds;
    [SerializeField] 
    private float _zonesDistance;
    [SerializeField] 
    private GameObject _zonesContainer;
    [SerializeField]
    private Zone _startZone;
    [SerializeField] 
    private int _zoneOnSceneCount;
    [SerializeField]
    private int _zoneCountOnStart;

    [ReadOnly, SerializeField]
    private Zone _currentZone;
    
    [ReadOnly, SerializeField]
    private Zone _newZone;
    
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
                _zoneList = new List<Zone> {_startZone};
                GenerateNextZone();
            }
            else
            {
                Debug.LogError("Generator doesn't have a start point");
            }
        }
        else
        {
            if (_zoneList.Count > _zoneOnSceneCount)
            {
                PoolManager.Instance.ZonePool.ReturnObject(_zoneList[0].gameObject);
                _zoneList.RemoveAt(0);
            }

            if (_zoneList.Count > 0)
            {
                InstantiateNextZone(_zoneList[_zoneList.Count - 1].transform.position);
                _newZone = _zoneList[_zoneList.Count - 1];
                _currentZone = _zoneList[_zoneList.Count - 2];
            } 
            else
            {
                Debug.LogError("Generator has empty zone list");
            }
        }
    }

    private void InstantiateNextZone(Vector2 previousZonePosition)
    {
        var zoneInstance = PoolManager.Instance.ZonePool.GetObject(
            previousZonePosition + Vector2.up * _zonesDistance).GetComponent<Zone>();
        zoneInstance.transform.parent = _zonesContainer.transform;
        zoneInstance.gameObject.SetActive(true);
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
