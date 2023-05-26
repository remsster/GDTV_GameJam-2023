using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    public int HealthPoints => healthPoints;

    public event Action DeathEvent;
    public event Action DamageEvent;

    public void TakeDamage(int damage)
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        DamageEvent?.Invoke();
        if (healthPoints == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DeathEvent?.Invoke();
    }
}
