using System;
using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    public event Action OnTabDownAction;
    public event Action OnTabUpAction;

    public event Action OnAltDownAction;
    public event Action OnAltUpAction;

    public event Action OnEnterDownAction;
    public event Action OnEnterUpAction;

    public void Start()
    {
        BindInputs();
    }

    void BindInputs()
    {
        InputBinder inputBinder = InputManager.Instance?.GetInputEventBinder(EInputActionAssetName.UI);
        inputBinder.BindInputEvent(EUIInputActionName.Inventory, OnTabInput);
        inputBinder.BindInputEvent(EUIInputActionName.UseMouse, OnAltInput);
        inputBinder.BindInputEvent(EUIInputActionName.Enter, OnEnterInput);
    }

    void OnTabInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnTabDownAction?.Invoke();
            InputManager.Instance.UseCursor();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnTabUpAction?.Invoke();
            InputManager.Instance.UnuseCursor();
        }
    }

    void OnAltInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnAltDownAction?.Invoke();
            InputManager.Instance.UseCursor();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnAltUpAction?.Invoke();
            InputManager.Instance.UnuseCursor();
        }
    }

    void OnEnterInput(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            OnEnterDownAction?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnEnterUpAction?.Invoke();
        }
    }


}
