using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MoveController
{
    private bool _fingerOnTouch;
    private Vector2 _firstFingerPosition;

    public override Vector2 GetVelocity()
    {
        if (_fingerOnTouch)
            return Camera.main.WorldToScreenPoint(Input.GetTouch(0).position - _firstFingerPosition);
        else
            return Vector2.zero;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _firstFingerPosition = GetFirstFingerPosition();
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                _fingerOnTouch = true;
            else
                _fingerOnTouch = false;
        }
    }

    private Vector2 GetFirstFingerPosition()
    {
        return Camera.main.WorldToScreenPoint(Input.GetTouch(0).position);
    }
}
