using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerTestScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    InputBinder inputBinder;
    private void Awake()
    {
        inputBinder = InputManager.Instance?.GetInputEventBinder(EInputActionAssetName.Player);


        if (inputBinder != null)
        {
            inputBinder.BindInputEvent(nameof(EInputMapName.Default), nameof(EPlayerInputActionName.Move), InputPlayerMove);
            inputBinder.BindInputEvent(nameof(EInputMapName.Default), nameof(EPlayerInputActionName.Attack), InputDefaultLeftMouse);
            inputBinder.BindInputEvent(nameof(EInputMapName.UseMouse), nameof(EPlayerInputActionName.MouseLeft), InputUseMouseLeftMouse);
        }
    }


    public void SwitchMapTest()
    {
        if(inputBinder.CurrenMapName == nameof(EInputMapName.Default))
            inputBinder.SwitchMap(nameof(EInputMapName.UseMouse));
        else
            inputBinder.SwitchMap(nameof(EInputMapName.Default));
    }

    public void InputPlayerMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        if(move.normalized != Vector2.zero)
            text.text = context.ReadValue<Vector2>().ToString();
    }
    

    
    public void InputDefaultLeftMouse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            text.text = "Player | Left Mouse Click";
        }
            
    }

    public void InputUseMouseLeftMouse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            text.text = "Use Mouse | Left Mouse Click";
        }
    }
    
    
}
