using UnityEngine;
using UnityEngine.InputSystem;

public enum Dimension
{
    TopDown = 1,
    SideScroll = 2
}

public class Player : MonoBehaviour
{

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Dimension currentDimension;

    private void Start()
    {
        currentDimension = Dimension.TopDown;
        // Debug.Log("Current Dimension: " + currentDimension);
    }

    private void Update()
    {
        if (currentDimension == Dimension.TopDown)
        {
            Rigidbody.gravityScale = 0;
            Rigidbody.velocity = new Vector2(InputReader.MovementValue.x * speed, InputReader.MovementValue.y * speed);
        }

        if (currentDimension == Dimension.SideScroll)
        {
            Rigidbody.velocity = new Vector2(InputReader.MovementValue.x * speed, Rigidbody.velocity.y);
            Rigidbody.gravityScale = 1;
        }
    }

    private void OnEnable()
    {
        InputReader.ChangeDimensionEvent += InputReader_ChangeDimensionEvent;
        InputReader.JumpEvent += InputReader_JumpEvent;
    }

    private void OnDisable()
    {
        InputReader.ChangeDimensionEvent -= InputReader_ChangeDimensionEvent;
        InputReader.JumpEvent -= InputReader_JumpEvent;
    }

    private void InputReader_ChangeDimensionEvent()
    {
        if (currentDimension == Dimension.TopDown)
        {
            PlayerInput.SwitchCurrentActionMap("SideScroll");
            currentDimension = Dimension.SideScroll;
        } 
        else if (currentDimension == Dimension.SideScroll)
        {
            PlayerInput.SwitchCurrentActionMap("TopDown");
            currentDimension = Dimension.TopDown;
        }
        Debug.Log("Current Dimension: " + currentDimension);
    }

    private void InputReader_JumpEvent()
    {
        if (currentDimension == Dimension.TopDown) return;
        Rigidbody.velocity = new Vector2(0, jumpForce);
    }
}
