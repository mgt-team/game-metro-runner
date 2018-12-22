using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField]
    private Player _player;
    
    private MoveController _moveController;
    private Animator _animator;
    
    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
        _player = GameObject.FindGameObjectWithTag(TagManager.GetTagNameByEnum(TagEnum.Player)).GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement()
    {
        Vector2 velocity = _moveController.GetVelocity();
        _player.SetVelocity(velocity);
        if (velocity == Vector2.zero)
            _player.StopAnimation();
        else
            _player.StartAnimation();
    }
}