using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    [SerializeField] ScMovement moveScript;
    private Vector2 lStick;

    public void GetLeftStick(InputAction.CallbackContext ctxt)
    {
        moveScript.GetMovementDirection(ctxt.ReadValue<Vector2>());
    }
}
