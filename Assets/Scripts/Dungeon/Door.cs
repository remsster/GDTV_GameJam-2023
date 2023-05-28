using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private BoxCollider2D collisionBox;
    [SerializeField] private BoxCollider2D triggerBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        spriteRenderer.sprite = unlockedSprite;
        collisionBox.enabled = false;
        triggerBox.enabled = false;
    }
}
