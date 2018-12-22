using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEnterController : MonoBehaviour
{
	[SerializeField]
	private ZoneManager _zoneManager;

    private void Awake()
    {
        _zoneManager = GameObject.FindGameObjectWithTag(TagManager.GetTagNameByEnum(TagEnum.ZoneManager)).GetComponent<ZoneManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (TagManager.CompareGameObjectTag(collision.gameObject, TagEnum.Zone))
		{
			Debug.Log("Enter the next zone");
			_zoneManager.GenerateNextZone();
		}
	}
}
