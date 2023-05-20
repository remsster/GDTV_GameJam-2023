using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MovementValue { get; private set; }

    public event Action ChangeDimensionEvent;
    public event Action JumpEvent;

    private void OnMovement(InputValue value)
    {
        MovementValue = value.Get<Vector2>();
    }

    private void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Hello");
        }
    }

    private void OnChangeDimension(InputValue value)
    {
        if (value.isPressed)
        {
            ChangeDimensionEvent?.Invoke();
        }
    }
    
}
