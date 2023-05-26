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
        if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.TakeDamage();
        }

    }

    public void SetDirection(Direction direction)
    {
        this.direction = direction;
    }
}
