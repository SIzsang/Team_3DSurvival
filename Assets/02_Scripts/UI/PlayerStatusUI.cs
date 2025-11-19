using _02_Scripts.Core.Managers;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{

    [SerializeField] private ConditionUI healthBar;
    [SerializeField] private ConditionUI staminaBar;
    [SerializeField] private ConditionUI hungerBar;
    [SerializeField] private ConditionUI thirstyBar;

    void Start()
    {
        PlayerStatus status = GameManager.Instance.Player.Status;
        
        healthBar.Init(status.HealthCondition);
        staminaBar.Init(status.StaminaCondition);
        hungerBar.Init(status.HungerCondition);
        thirstyBar.Init(status.ThirstyCondition);
    }
    
    
}
