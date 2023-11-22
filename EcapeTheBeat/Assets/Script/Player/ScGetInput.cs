using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScMovement moveScript;
    private ScAttack attackScript;
    private Vector2 lStick;
    private Vector2 RStick;


    private void Start()
    {
        moveScript = GetComponent<ScMovement>();
        attackScript = GetComponent<ScAttack>();
    }
    public void GetLeftStick(InputAction.CallbackContext ctxt)
    {
        if (ctxt.ReadValue<Vector2>() != Vector2.zero)
        lStick = ctxt.ReadValue<Vector2>();

        moveScript.GetMovementDirection(ctxt.ReadValue<Vector2>());
    }

    public void GetRightStick(InputAction.CallbackContext ctxt) 
    {
        RStick = ctxt.ReadValue<Vector2>();
        attackScript.GetAimingDirection(RStick);
    }

    public void GetLeftTrigger(InputAction.CallbackContext ctxt)
    {
        if (ctxt.ReadValueAsButton())
        {
                moveScript.GetDashInstruction(lStick);
        }
    }

    public void GetRightTrigger(InputAction.CallbackContext ctxt)
    { 
        if (ctxt.started)
        {
            attackScript.Attack(true);
        }

        if (ctxt.canceled)
        {
            attackScript.Attack(false);
        }
    }
}
