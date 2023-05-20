using UnityEngine;

public enum Dimension
{
    TopDown = 1,
    SideScroll = 2
}

public class Player : MonoBehaviour
{

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Rigidbody2D Rigidbody { get; private set; }

    [SerializeField] private float speed;

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
    }

    private void OnDisable()
    {
        InputReader.ChangeDimensionEvent -= InputReader_ChangeDimensionEvent;
    }

    private void InputReader_ChangeDimensionEvent()
    {
        Debug.Log("Dimension Changed");
        if (currentDimension == Dimension.TopDown)
        {
            currentDimension = Dimension.SideScroll;
        } 
        else if (currentDimension == Dimension.SideScroll)
        {
            currentDimension = Dimension.TopDown;
        }
        Debug.Log("Current Dimension: " + currentDimension);
    }
}
