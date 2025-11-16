using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get => instance;
        set => instance = value;
    }


    private Dictionary<EInputActionAssetName,InputBinder> inputBinders;
    
    public bool IsUIOpen => isUIOpen;
    bool isUIOpen = false;
    
    
    private void Awake()
    {
        if ( instance == null )
        {
            instance = this;
            
            // BootstrapScene 이미 Dontdestroy 오브젝트안에 들어가있음
            //DontDestroyOnLoad(this);

            Init();
        }
        else if ( instance != this )
        {
            Destroy( gameObject );
        }
    }

    void Init()
    {
        inputBinders = new();
        InputBinder[] binders = GetComponentsInChildren<InputBinder>();

        foreach (InputBinder binder in binders)
        {
            if (Enum.TryParse(binder.gameObject.name, out EInputActionAssetName result))
            {
                if(inputBinders.ContainsKey(result)) continue;
                inputBinders.Add(result, binder);
            }
        }
    }

    public InputBinder GetInputEventBinder(EInputActionAssetName actionAssetName)
    {
        if(inputBinders.ContainsKey(actionAssetName))
            return inputBinders[actionAssetName];
        return null;
    }
    
    
    
    // public void OpenUI()
    // {
    //     isUIOpen = true;
    //     Cursor.lockState = CursorLockMode.None;
    // }
    //
    // public void CloseUI()
    // {
    //     isUIOpen = false;
    //     Cursor.lockState = CursorLockMode.Locked;
    // }
}