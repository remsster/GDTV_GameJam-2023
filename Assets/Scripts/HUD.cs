using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI keysText;

    [SerializeField] private PlayerController player;

    // ---------------------------------------------
    // Unity Engine
    // ---------------------------------------------

    private void Start()
    {
        UpdateHUD();
    }

    private void OnEnable()
    {
        player.Health.DamageEvent += Player_Health_DamageEvent;
        player.KeyCollectedEvent += Player_KeyCollectedEvent;
    }

    private void OnDisable()
    {
        player.Health.DamageEvent -= Player_Health_DamageEvent;
        player.KeyCollectedEvent -= Player_KeyCollectedEvent;
    }

    // ---------------------------------------------
    // Events
    // ---------------------------------------------

    private void Player_Health_DamageEvent()
    {
        UpdateHUD();
    }

    private void Player_KeyCollectedEvent()
    {
        UpdateHUD();
    }

    // ---------------------------------------------
    // Other
    // ---------------------------------------------

    public void UpdateHUD()
    {
        livesText.text = "Lives: " + player.Health.HealthPoints;
        keysText.text = "Keys: " + player.Keys;
    }
}
