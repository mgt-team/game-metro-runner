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
            return (GetFingerPosition() - _firstFingerPosition).normalized;
        else
            return Vector2.zero;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
                _firstFingerPosition = GetFingerPosition();
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                _fingerOnTouch = true;
            else if (touch.phase == TouchPhase.Ended)
                _fingerOnTouch = false;
        }
    }

    private Vector2 GetFingerPosition()
    {
        return Camera.main.WorldToScreenPoint(Input.GetTouch(0).position);
    }
}
