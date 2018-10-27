using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

	[SerializeField]
	private Camera _camera;

	[SerializeField] 
	private float _speed;
	
	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		_camera.transform.position += new Vector3(0, _speed * Time.deltaTime);
	}
}
