using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MovementValue { get; private set; }

    public event Action ChangeDimensionEvent;
    public event Action JumpEvent;
    public event Action AttackEvent;

    private void OnMovement(InputValue value)
    {
        MovementValue = value.Get<Vector2>();
    }

    private void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            AttackEvent?.Invoke();
        }
    }

    private void OnChangeDimension(InputValue value)
    {
        if (value.isPressed)
        {
            ChangeDimensionEvent?.Invoke();
        }
    }

    // SideScroll Controls

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            JumpEvent?.Invoke();
        }
    }    
    
}
