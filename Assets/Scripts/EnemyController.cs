using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [field: SerializeField] public Health Health { get; private set; }

    // ---------------------------------------------
    // Unity Engine
    // ---------------------------------------------

    private void OnEnable()
    {
        Health.DeathEvent += Health_DeathEvent;
    }

    private void OnDisable()
    {
        Health.DeathEvent -= Health_DeathEvent;
    }

    // ---------------------------------------------
    // Events
    // ---------------------------------------------

    private void Health_DeathEvent()
    {
        Destroy(gameObject);
    }

    // ---------------------------------------------
    // Other
    // ---------------------------------------------

    public void TakeDamage()
    {
        Health.TakeDamage(10);
    }



}
