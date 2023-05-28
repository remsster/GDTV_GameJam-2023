using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ColorDimension
{
    Red,Blue
}

public enum Direction
{
    Up, Down, Left, Right
}

public class PlayerController : MonoBehaviour
{

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float attackTimer;
    

    [SerializeField] private GameObject attackEffector;

    [Header("Attack Directions")]
    [SerializeField] private Transform attackLeft;
    [SerializeField] private Transform attackRight;
    [SerializeField] private Transform attackUp;
    [SerializeField] private Transform attackDown;

    private Direction direction;

    private int keys = 0;
    private bool canAttack = true;
    private bool isAttacking = false;
    private float timer = 0;

    private Door currentDoor;

    [HideInInspector] public int Keys => keys;

    public event Action KeyCollectedEvent;

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
        if (timer > attackTimer)
        {
            canAttack = true;
        }
        timer += Time.deltaTime;
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
        if (!canAttack) return;
        Rigidbody.velocity = Vector2.zero;
        Transform transfrom = direction switch
        {
            Direction.Up => attackUp,
            Direction.Down => attackDown,
            Direction.Left => attackLeft,
            Direction.Right => attackRight,
            _ => null
        };
        if (transform == null) Debug.LogError("Attack Transform is null");
        GameObject af = Instantiate(attackEffector, transfrom.position, Quaternion.identity);
        af.GetComponent<AttackEffector>().SetDirection(direction);
        StartCoroutine(PlayAttackAnimation());
        timer = 0;
        canAttack = false;
    }

    private void InputReader_InteractEvent()
    {
        if (currentDoor != null && keys > 0 && direction == Direction.Up)
        {
            currentDoor.Unlock();
            keys--;
            KeyCollectedEvent?.Invoke();
            currentDoor = null;
        }
    }

    // ---------------------------------------------
    // Other
    // ---------------------------------------------

    private void Move()
    {
        if (isAttacking) return;
        switch (InputReader.MovementValue)
        {
            case Vector2 v when v.Equals(Vector2.up):
                Animator.Play("up walk");
                SpriteRenderer.flipX = false;
                direction = Direction.Up;
                break;
            case Vector2 v when v.Equals(Vector2.down):
                direction = Direction.Down;
                SpriteRenderer.flipX = false;
                Animator.Play("down walk");
                break;
            case Vector2 v when v.Equals(Vector2.left):
                direction = Direction.Left;
                SpriteRenderer.flipX = true;
                Animator.Play("side walk");
                break;
            case Vector2 v when v.Equals(Vector2.right):
                direction = Direction.Right;
                SpriteRenderer.flipX = false;
                Animator.Play("side walk");
                break;
            default:
                PlayIdleAnimation();
                break;
        }
        
        Rigidbody.velocity = new Vector2(InputReader.MovementValue.x * speed, InputReader.MovementValue.y * speed);
    }

    private IEnumerator PlayAttackAnimation()
    {
        isAttacking = true;
        if (direction == Direction.Up)
        {
            SpriteRenderer.flipX = false;
            Animator.Play("attack up");
        }
        else if (direction == Direction.Down)
        {
            SpriteRenderer.flipX = false;
            Animator.Play("attack down");
        }
        else if (direction == Direction.Left)
        {
            SpriteRenderer.flipX = true;
            Animator.Play("attack left");
        }
        else if (direction == Direction.Right)
        {
            SpriteRenderer.flipX = false;
            Animator.Play("attack right");
        }
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    private void PlayIdleAnimation()
    {
        if (direction == Direction.Up)
        {
            SpriteRenderer.flipX = false;
            Animator.Play("up idle");
        }
        else if (direction == Direction.Down)
        {
            SpriteRenderer.flipX = false;
            Animator.Play("down idle");
        }
        else if (direction == Direction.Left)
        {
            SpriteRenderer.flipX = true;
            Animator.Play("side idle");
        }
        else if (direction == Direction.Right)
        {
            SpriteRenderer.flipX = false;
            Animator.Play("side idle");
        }
    }

    private void IncreaseKeys()
    {
        keys++;
        KeyCollectedEvent?.Invoke();
    }

    private void DecreaseKeys()
    {
        keys--;
    }
}
