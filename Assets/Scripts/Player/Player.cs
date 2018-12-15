using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {
	
	[SerializeField]
	private Vector2 _shiftPower;	

	private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public float _forcePower;

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
        if (Input.GetKeyDown(KeyCode.Space))
            Force();
	}

    public void Force()
    {
        //Эти два метода работают
        //_rigidbody.velocity *= _forcePower;       
        //_rigidbody.AddForce(Vector2.up * _forcePower);                
    }
	
	public void SetVelocity(Vector2 direction)
	{
		if (direction.x != 0 && direction.y != 0)
			direction *= Mathf.Sqrt(2) / 2;
		
		_rigidbody.velocity = new Vector2(direction.x * _shiftPower.x, direction.y * _shiftPower.y);
	}

    private void GetCoin(GameObject coin)
    {
        Destroy(coin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagManager.GetTagNameByEnum(TagEnum.Coin))
        {
            GetCoin(collision.gameObject);
        }
    }
}
