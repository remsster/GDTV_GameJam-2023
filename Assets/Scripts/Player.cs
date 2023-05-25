using UnityEngine;
using UnityEngine.InputSystem;

public enum ColorDimension
{
    Red,Blue
}

public class Player : MonoBehaviour
{

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private int keys = 0;

    private Door currentDoor;

    // ---------------------------------------------
    // Unity Engine
    // ---------------------------------------------
  
    private void Update()
    {
        Debug.Log("Keys Count: " + keys);
        Move();
    }

    private void OnEnable()
    {
        InputReader.ChangeDimensionEvent += InputReader_ChangeDimensionEvent;
        InputReader.InteractEvent += InputReader_InteractEvent;
    }

    private void OnDisable()
    {
        InputReader.ChangeDimensionEvent -= InputReader_ChangeDimensionEvent;
        InputReader.InteractEvent -= InputReader_InteractEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Pickup>(out Pickup pickup))
        {
            if (pickup.Category == PickupCategory.Key)
            {
                IncreaseKeys();
            }
        }

        if (collision.TryGetComponent<Door>(out Door door))
        {
            currentDoor = door;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Door>(out Door door))
        {
            currentDoor = null;
        }
    }

    // ---------------------------------------------
    // Events
    // ---------------------------------------------

    private void InputReader_ChangeDimensionEvent()
    {        
    }

    private void InputReader_InteractEvent()
    {
        if (currentDoor != null && keys > 0)
        {
            Destroy(currentDoor.gameObject);
            currentDoor = null;
        }
    }

    // ---------------------------------------------
    // Other
    // ---------------------------------------------

    private void Move()
    {
        Rigidbody.velocity = new Vector2(InputReader.MovementValue.x * speed, InputReader.MovementValue.y * speed);
    }

    private void IncreaseKeys()
    {
        keys++;
    }

    private void DecreaseKeys()
    {
        keys--;
    }
}
