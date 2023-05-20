using UnityEngine;

public enum Dimension
{
    TopDown = 1,
    SideScroll = 2
}

public class Player : MonoBehaviour
{

    [field:SerializeField] public InputReader InputReader { get; private set; }

    private Dimension currentDimension;

    private void Start()
    {
        currentDimension = Dimension.TopDown;
        // Debug.Log("Current Dimension: " + currentDimension);
    }

    private void Update()
    {
        // Debug.Log("MovementValue" + InputReader.MovementValue);
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

        if (currentDimension == Dimension.SideScroll)
        {
            currentDimension = Dimension.TopDown;
        }
        Debug.Log("Current Dimension: " + currentDimension);
    }
}
