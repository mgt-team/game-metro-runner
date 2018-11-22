using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    private ZoneProperties m_zoneProperties;
    private SpriteRenderer spriteRenderer;
    private ZoneGrid _zoneGrid;

    private List<List<ZonePoint>> _zonePoints;

    public int x;
    public int y;

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
        _zonePoints = _zoneGrid.GetGrid(transform.position, 10, 10);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.M) && m_zoneProperties != null)
        {
            _zoneGrid.GetVisualGrid(transform.position, 10, 10);
            if (_zonePoints != null)
                Debug.Log(_zonePoints[y][x].IsFree());              //Check for working of class ZonePoint
        }       
    }
}
