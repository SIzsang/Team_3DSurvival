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


    private Dictionary< EInputActionAssetName, InputBinder > inputBinders;

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
        InputBinder[] binders = GetComponentsInChildren< InputBinder >();

        foreach ( InputBinder binder in binders )
        {
            if ( Enum.TryParse( binder.gameObject.name, out EInputActionAssetName result ) )
            {
                if ( inputBinders.ContainsKey( result ) ) continue;
                binder.SetEnableInput(true);
                inputBinders.Add( result, binder );
                
            }
        }
    }

    public InputBinder GetInputEventBinder( EInputActionAssetName actionAssetName )
    {
        if ( inputBinders.ContainsKey( actionAssetName ) )
            return inputBinders[ actionAssetName ];
        return null;
    }


    /// <summary>
    /// ex) 캐릭터 움직임 막고 UI Input만 할 때,
    /// InputManager.Instance.SetDisable(EInputActionAssetName.Player);
    /// InputManager.Instance.SetEnableInput(EInputActionAssetName.UI);
    /// </summary>
    /// <param name="actionAssetName"></param>
    /// <param name="onlyThis"></param>
    public void EnableInput( EInputActionAssetName actionAssetName )
    {
        inputBinders[actionAssetName]?.SetEnableInput(true);
    }

    public void DiableInput( EInputActionAssetName actionAssetName )
    {
        inputBinders[actionAssetName]?.SetEnableInput(false);
    }

    public void EnabeAllInput()
    {
        foreach ( var binder in inputBinders.Values )
        {
            binder.SetEnableInput( true );
        }
    }

    public void DisableAllInput()
    {
        foreach ( var binder in inputBinders.Values )
        {
            binder.SetEnableInput( false );
        }
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