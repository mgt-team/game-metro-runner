using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    private ZoneProperties m_zoneProperties;
    private SpriteRenderer spriteRenderer;
    private ZoneGrid _zoneGrid;

    private List<List<ZonePoint>> _zonePoints;

    [SerializeField]
    private int _rows;

    [SerializeField]
    private int _coloms;

    public void SetZoneProperties(ZoneProperties zoneProperties)
    {
        m_zoneProperties = zoneProperties;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (m_zoneProperties == null)
            return;
        GenerateEnvironment();
        GetGrid(m_zoneProperties.background);
    }

    public void GenerateEnvironment()
    {
        GenerateBackground(m_zoneProperties.background);
    }

    public void GenerateBackground(Sprite background)
    {
        spriteRenderer.sprite = background;
    }

    private void GetGrid(Sprite background)
    {
        _zoneGrid = new ZoneGrid(background);
        _zonePoints = _zoneGrid.GetGrid(transform.position, _coloms, _rows);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            _zoneGrid.GetVisualGrid(transform.position, _coloms, _rows);
            if (_zonePoints != null)
                Debug.Log(_zonePoints[3][3].GetPointPosition());              //Check for working of class ZonePoint
        }       
    }
}
