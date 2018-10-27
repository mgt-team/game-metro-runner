using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField]
    private Player _player;
    
    private MoveController _moveController;
    
    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement()
    {
        _player.SetVelocity(_moveController.GetVelocity());
    }
}