using UnityEngine;

public enum PickupCategory
{
    Key,
    Coin
}


public class Pickup : MonoBehaviour
{

    [SerializeField] private PickupCategory pickupCategory;

    public PickupCategory Category => pickupCategory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
