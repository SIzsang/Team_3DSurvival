using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [ SerializeField ] private PlayerStatusData playerStatusData;

    public float MoveSpeed => playerStatusData.moveSpeed;
    public float JumpForce => playerStatusData.jumpForce;
    public float DashMultiplier => playerStatusData.dashMultiplier;
    public float DashStaminaCost => playerStatusData.DashStaminaCost;
    public float JumpStaminaCost => playerStatusData.JumpStaminaCost;
    public float AttackStaminaCost => playerStatusData.AttackStaminaCost;
    public float AttackDealy => playerStatusData.AttackDelay;

    public float NowHealth => health.CurrentValue;
    public float NowStamina => stamina.CurrentValue;
    public float NowThirsty => thirsty.CurrentValue;
    public float NowHunger => hunger.CurrentValue;


    public Condition HealthCondition => health;
    public Condition StaminaCondition => stamina;
    public Condition ThirstyCondition => thirsty;
    public Condition HungerCondition => hunger;
    
    private Condition health;
    private Condition stamina;
    private Condition thirsty;
    private Condition hunger;

    private void Awake()
    {
        health = new Condition( playerStatusData.maxHealth, playerStatusData.healthNaturalRecovery, playerStatusData.healthNaturalRecoveryRate );
        stamina = new Condition( playerStatusData.maxStamina, playerStatusData.staminaNaturalRecovery, playerStatusData.staminaNaturalRecoveryRate );
        thirsty = new Condition( playerStatusData.maxThirsty, playerStatusData.thirstyDecay, playerStatusData.thirstyDecayRate );
        hunger = new Condition( playerStatusData.maxHunger, playerStatusData.hungerDecay, playerStatusData.hungerDecayRate );

        StartNaturalChangeRoutin( health );
        StartNaturalChangeRoutin( stamina );
        StartNaturalChangeRoutin( thirsty );
        StartNaturalChangeRoutin( hunger );
    }

    public void AddHealth( int damage )
    {
        health.AddCurrentValue( damage );
    }

    public void AddStamina( float amount )
    {
        stamina.AddCurrentValue( amount );
    }

    public void AddThirsty( float amount )
    {
        thirsty.AddCurrentValue( amount );
    }

    public void AddHunger( float amount )
    {
        hunger.AddCurrentValue( amount );
    }

    public void StartNaturalChangeRoutin( Condition condition )
    {
        if ( condition.ChangeRoutine != null )
        {
            StopCoroutine( condition.ChangeRoutine );
        }

        Coroutine newCoroutine = StartCoroutine( NaturalChangeRoutine( condition ) );
        condition.SetChangeRoutine( newCoroutine );
    }

    IEnumerator NaturalChangeRoutine( Condition targetCondition )
    {
        while ( true )
        {
            yield return new WaitForSeconds( targetCondition.NaturalChangeRate );
            targetCondition.AddNaturalChangeValue( targetCondition.NaturalChangeValue );
            
            while ( true )
            {
                if ( targetCondition.IsUsing == true )
                {
                    targetCondition.SetUsingCondition( false );
                    yield return new WaitForSeconds( 3.0f );
                }

                if ( targetCondition.IsUsing == false )
                    break;
            }
        }
    }
    
    public void StartOtherChangeRoutine(Condition condition, float amount, float rate)
    {
        if ( condition.ChangeRoutine != null )
        {
            StopCoroutine( condition.ChangeRoutine );
        }

        Coroutine newCoroutine = StartCoroutine( OtherChangeRoutine( condition, amount, rate) );
        condition.SetChangeRoutine( newCoroutine );
    }

    public void StopOtherChangeRoutine(Condition condition)
    {
        if ( condition.ChangeRoutine != null )
        {
            StopCoroutine( condition.ChangeRoutine );
        }
        
        StartNaturalChangeRoutin( condition );
    }
    
    IEnumerator OtherChangeRoutine( Condition targetCondition, float amount, float rate )
    {
        while ( true )
        {
            targetCondition.AddNaturalChangeValue(amount);
            yield return new WaitForSeconds(rate);
        }
    }

    public void StartDash()
    {
        StartOtherChangeRoutine(stamina, -DashStaminaCost, playerStatusData.DashStaminaRate);
    }

    public void StopDash()
    {
        StopOtherChangeRoutine(stamina);
    }
}