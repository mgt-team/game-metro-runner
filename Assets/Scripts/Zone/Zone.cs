using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Zone : MonoBehaviour {
    private ZoneProperties m_zoneProperties;
    private SpriteRenderer spriteRenderer;
    private ZoneGrid _zoneGrid;

    [SerializeField]
    private int _rows;

    [FormerlySerializedAs("_colums")] [FormerlySerializedAs("_coloms")] [SerializeField]
    private int _columns;

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
        _zoneGrid = new ZoneGrid(background, gameObject.transform.position, _columns, _rows);
    }

    public ZoneGrid GetGrid()
    {
        return _zoneGrid;
    }
}
