using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScMovement moveScript;
    private ScCanon canonScript;
    private Vector2 lStick;
    private Vector2 RStick;


    private void Start()
    {
        moveScript = GetComponent<ScMovement>();
        canonScript = GetComponent<ScCanon>();
    }
    public void GetLeftStick(InputAction.CallbackContext ctxt)
    {
        lStick = ctxt.ReadValue<Vector2>();
        moveScript.GetMovementDirection(lStick);
    }

    public void GetRightStick(InputAction.CallbackContext ctxt) 
    {
        RStick = ctxt.ReadValue<Vector2>();
        canonScript.GetAimingDirection(RStick);
    }
}
