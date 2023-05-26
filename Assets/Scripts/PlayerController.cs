using UnityEngine;
using UnityEngine.InputSystem;

public enum ColorDimension
{
    Red,Blue
}

enum Direction
{
    Up, Down, Left, Right
}

public class PlayerController : MonoBehaviour
{

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject attackEffector;

    [Header("Attack Directions")]
    [SerializeField] private Transform attackLeft;
    [SerializeField] private Transform attackRight;
    [SerializeField] private Transform attackUp;
    [SerializeField] private Transform attackDown;

    private Direction direction;

    private int keys = 0;

    private Door currentDoor;

    // ---------------------------------------------
    // Unity Engine
    // ---------------------------------------------

    private void Start()
    {
        direction = Direction.Up;
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        InputReader.ChangeDimensionEvent += InputReader_ChangeDimensionEvent;
        InputReader.InteractEvent += InputReader_InteractEvent;
        InputReader.AttackEvent += InputReader_AttackEvent;
    }

    private void OnDisable()
    {
        InputReader.ChangeDimensionEvent -= InputReader_ChangeDimensionEvent;
        InputReader.InteractEvent -= InputReader_InteractEvent;
        InputReader.AttackEvent -= InputReader_AttackEvent;
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

    private void InputReader_AttackEvent()
    {
        Transform transfrom = direction switch
        {
            Direction.Up => attackUp,
            Direction.Down => attackDown,
            Direction.Left => attackLeft,
            Direction.Right => attackRight,
            _ => null
        };
        if (transform == null) Debug.LogError("Attack Transform is null");
        Instantiate(attackEffector, transfrom.position, Quaternion.identity);
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
        switch (InputReader.MovementValue)
        {
            case Vector2 v when v.Equals(Vector2.up):
                direction = Direction.Up;
                break;
            case Vector2 v when v.Equals(Vector2.down):
                direction = Direction.Down;
                break;
            case Vector2 v when v.Equals(Vector2.left):
                direction = Direction.Left;
                break;
            case Vector2 v when v.Equals(Vector2.right):
                direction = Direction.Right;
                break;
        }
        
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
