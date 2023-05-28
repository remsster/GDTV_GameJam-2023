using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Door door;

    private void Start()
    {
        Debug.Log("Hello");
    }

    public void UnlockDoor()
    {
        Debug.Log("Called");
        door.Unlock();
    }
}
