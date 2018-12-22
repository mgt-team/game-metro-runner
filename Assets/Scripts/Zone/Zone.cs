using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Zone : MonoBehaviour {
    private ZoneProperties _zoneProperties;
    private SpriteRenderer _spriteRenderer;
    private ZoneGrid _zoneGrid;

    [SerializeField]
    private int _rows;

    [SerializeField]
    private int _columns;

    public void SetZoneProperties(ZoneProperties zoneProperties)
    {
        _zoneProperties = zoneProperties;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitZone()
    {
        if (_zoneProperties == null)
        {
            return;
        }
        
        InitGrid(_zoneProperties.background);
        GenerateEnvironment();
    }

    public void GenerateEnvironment()
    {
        GenerateBackground(_zoneProperties.background);
        ZoneObjectsGenerator.Instance.GenerateOnGrid(_zoneGrid);
    }

    public void GenerateBackground(Sprite background)
    {
        _spriteRenderer.sprite = background;
    }

    private void InitGrid(Sprite background)
    {
        _zoneGrid = new ZoneGrid(background, gameObject.transform.position, _columns, _rows);
    }

    public ZoneGrid GetGrid()
    {
        return _zoneGrid;
    }
}
