using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Condition
{
    public float CurrentValue => curValue;
    public float MaxValue => maxValue;
    public float NaturalChangeValue => naturalChangeValue;
    public float NaturalChangeRate => naturalChangeRate;


    public bool IsUsing => isUsing;
    private bool isUsing;

    private float curValue;
    private float maxValue;
    private float naturalChangeValue;
    private float naturalChangeRate;

    private bool isZeroEffective;
    private Condition zeroTargetCondition;


    public Condition(float _max, float _naturalChangeValue, float _naturalChangeRate, bool _isZeroEffective = false, Condition _zeroTargetCondition = null)
    {
        maxValue = _max;
        naturalChangeValue = _naturalChangeValue;
        naturalChangeRate = _naturalChangeRate;
        curValue = maxValue;
        
        isZeroEffective = _isZeroEffective;
        zeroTargetCondition = _zeroTargetCondition;
    }


    public void AddNaturalChangeValue(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0f, maxValue);
    }

    public void AddCurrentValue(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0f, maxValue);
        isUsing = true;
    }

    public void SetCurrentValue(float amount)
    {
        curValue = Mathf.Clamp(amount, 0f, maxValue);
        isUsing = true;
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void SetUsingCondition(bool _isUsing)
    {
        isUsing = _isUsing;
    }


    public Coroutine ChangeRoutine => changeRoutine;
    private Coroutine changeRoutine;

    public void SetChangeRoutine(Coroutine routine)
    {
        changeRoutine = routine;
    }

}
