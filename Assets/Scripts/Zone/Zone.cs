using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    private ZoneProperties m_zoneProperties;
    private SpriteRenderer spriteRenderer;
    private ZoneGrid _zoneGrid;

    public List<Vector2> _positions;

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
        _positions = _zoneGrid.GetGrid(transform.position);
    }
}
