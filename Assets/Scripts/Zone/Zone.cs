using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    private ZoneProperties m_zoneProperties;
    private SpriteRenderer spriteRenderer;
    private ZoneGrid _zoneGrid;

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
        InitGrid(m_zoneProperties.background);
    }

    public void GenerateEnvironment()
    {
        GenerateBackground(m_zoneProperties.background);
    }

    public void GenerateBackground(Sprite background)
    {
        spriteRenderer.sprite = background;
    }

    private void InitGrid(Sprite background)
    {
        _zoneGrid = new ZoneGrid(background, gameObject.transform.position, _coloms, _rows);
    }

    public ZoneGrid GetGrid()
    {
        return _zoneGrid;
    }
}
