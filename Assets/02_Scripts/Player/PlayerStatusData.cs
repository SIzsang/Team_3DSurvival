using UnityEngine;


[ CreateAssetMenu( fileName = "New PlayerStatusData", menuName = "Scriptable Objects/Player/PlayerStatusData" ) ]
public class PlayerStatusData : ScriptableObject
{
    [ Header( "Status" ) ]
    public int maxHealth = 100;
    public float healthNaturalRecoveryRate = 1.0f;
    public int healthNaturalRecovery = 0;

    [Space(5)]
    public int maxStamina = 50;
    public float staminaNaturalRecoveryRate = .5f;
    public int staminaNaturalRecovery = 1;

    [Space(5)]
    public int maxThirsty = 50;
    public float thirstyDecayRate = 3f;
    public int thirstyDecay = -1;

    [Space(5)]
    public int maxHunger = 50;
    public float hungerDecayRate = 5f;
    public int hungerDecay = -1;

    [Space(10)]
    public int defaultAtk = 2;
    
    [ Header( "Behaviour" ) ]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float dashMultiplier = 1.5f;

    [Space(5)]
    public float DashStaminaCost = 2.0f;
    public float DashStaminaRate = 0.5f;
    public float JumpStaminaCost = 3.0f;
    public float AttackStaminaCost = 1.0f;

    [Space(5)]
    public float AttackDelay = 0.5f;



}