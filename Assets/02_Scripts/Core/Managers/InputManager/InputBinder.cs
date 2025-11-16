using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

// 이름... 
[ RequireComponent( typeof( PlayerInput ) ) ]
public class InputBinder : MonoBehaviour
{
    PlayerInput playerInput;
    private Dictionary< string, InputActionMap > actionMaps;

    List< (InputAction, Action< InputAction.CallbackContext >) > nowBindingActions;


    public string CurrenMapName => playerInput.currentActionMap.name;

    // MonoBehaviour가 아니어도 될 것 같음...
    // 일단 고!
    private void Awake()
    {
        playerInput = GetComponent< PlayerInput >();

        ReadOnlyArray< InputActionMap > actionMap = playerInput.actions.actionMaps;
        actionMaps = new Dictionary< string, InputActionMap >();
        foreach ( InputActionMap map in actionMap )
        {
            actionMaps.Add( map.name, map );
        }

        nowBindingActions = new();
        SceneManager.sceneUnloaded += OnUnloadScene;
    }


    public void BindInputEvent( string mapName, string actionName, Action< InputAction.CallbackContext > action )
    {
        if ( actionMaps.TryGetAction( mapName, actionName, out InputAction inputAction ) )
        {
            inputAction.started += action;
            inputAction.performed += action;
            inputAction.canceled += action;

            nowBindingActions.Add( ( inputAction, action ) );
        }
    }

    public void BindInputEvent( string actionName, Action< InputAction.CallbackContext > action )
    {
        if ( actionMaps.TryGetAction( nameof( EInputMapName.Default ), actionName, out InputAction inputAction ) )
        {
            inputAction.started += action;
            inputAction.performed += action;
            inputAction.canceled += action;
        }
    }

    /// <summary>
    /// 해당 PlayerInput 활성/비활성화
    /// </summary>
    /// <param name="enable"></param>
    public void SetEnableInput( bool enable, bool allActionsEanble = true )
    {
        playerInput.enabled = enable;

        if ( enable == true && allActionsEanble == true )
        {
            foreach ( var a in actionMaps.Values )
            {
                a.Enable();
            }
        }
    }

    public void SwitchMap( string mapName, bool allActionsEnable = true )
    {
        playerInput.SwitchCurrentActionMap( mapName );

        if ( allActionsEnable )
        {
            foreach ( var a in playerInput.currentActionMap.actions )
            {
                a.Enable();
            }
        }
    }

    /// <summary>
    /// map 안의 action 만 따로 enable, disable 할 수 있도록
    /// </summary>
    /// <param name="mapName"></param>
    /// <param name="enable"></param>
    public void SetEnableInputAction( string mapName, string actionName, bool enable )
    {
        if ( actionMaps.TryGetAction( mapName, actionName, out InputAction action ) )
        {
            if ( enable )
                action.Enable();
            else
                action.Disable();
        }
    }

    public void OnUnloadScene( Scene scene )
    {
        for ( int i = nowBindingActions.Count - 1; i >= 0; i-- )
        {
            nowBindingActions[ i ].Item1.started -= nowBindingActions[ i ].Item2;
            nowBindingActions[ i ].Item1.performed -= nowBindingActions[ i ].Item2;
            nowBindingActions[ i ].Item1.canceled -= nowBindingActions[ i ].Item2;
            nowBindingActions.Remove( nowBindingActions[ i ] );
        }
    }
}