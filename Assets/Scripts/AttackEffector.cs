using UnityEngine;

public class AttackEffector : MonoBehaviour
{
    [SerializeField] private Sprite swordUp;
    [SerializeField] private Sprite swordDown;
    [SerializeField] private Sprite swordLeft;
    [SerializeField] private Sprite swordRight;
    [SerializeField] private SpriteRenderer spriteRenderer; 

    [SerializeField] private float destroyTimer = 0.5f;

    private Direction direction;

    void Start()
    {
        if (direction == Direction.Up) spriteRenderer.sprite = swordUp;
        else if (direction == Direction.Down)
        {
            spriteRenderer.sortingOrder = 3;
            spriteRenderer.sprite = swordDown;
        }
        else if (direction == Direction.Left) spriteRenderer.sprite = swordLeft;
        else if (direction == Direction.Right) spriteRenderer.sprite = swordRight;
        Destroy(gameObject,destroyTimer);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            Debug.Log("Hit an enemy");
            enemy.TakeDamage();
            return;
        }

        if (collision.gameObject.TryGetComponent<DoorSwitch>(out DoorSwitch doorSwitch))
        {
            Debug.Log("Hit a switch");
            doorSwitch.UnlockDoor();
            return;
        }

    }

    public void SetDirection(Direction direction)
    {
        this.direction = direction;
    }
}
