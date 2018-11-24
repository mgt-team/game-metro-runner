using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {
	
	[SerializeField]
	private Vector2 _shiftPower;	

	private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		
	}

    public void StopAnimation()
    {
        _animator.SetBool("isRun", false);
    }

    public void StartAnimation()
    {
        _animator.SetBool("isRun", true);
    }
	
	// Update is called once per frame
	void Update () {

	}
	
	public void SetVelocity(Vector2 direction)
	{
		if (direction.x != 0 || direction.y != 0)
			direction *= Mathf.Sqrt(2) / 2;

		var shiftPowerY = _shiftPower.y;

		if(direction.y < 0)
		{
			shiftPowerY *= 2;
		}
		else
		{
			shiftPowerY *= 1.5f;
		}
		
		_rigidbody.velocity = new Vector3(direction.x * _shiftPower.x, direction.y * shiftPowerY);
	}
}
