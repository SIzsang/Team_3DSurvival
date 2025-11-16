using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player player;
    private InputBinder inputBinder;
    private PlayerBehaviour behaviour;

    public void Init( Player _player )
    {
        player = _player;
        behaviour = player.Behaviour;
        BindInputs();
    }

    void BindInputs()
    {
        inputBinder = InputManager.Instance?.GetInputEventBinder( EInputActionAssetName.Player );
        
        inputBinder?.BindInputEvent( nameof( EPlayerInputActionName.Move ), OnMove );
        inputBinder?.BindInputEvent(nameof(EPlayerInputActionName.Jump), OnJump  );
        
    }

    void OnMove( InputAction.CallbackContext context )
    {
        behaviour.SetInputDirection( context.ReadValue< Vector2 >() );
    }

    void OnJump( InputAction.CallbackContext context )
    {
        if(context.phase == InputActionPhase.Started)
            behaviour.Jump();
    }

    void OnAttack( InputAction.CallbackContext context )
    {
    }

    // InteractionDetector 로 이동 필요
    void OnInteraction( InputAction.CallbackContext context )
    {
    }


    void OnDash( InputAction.CallbackContext context )
    {
        if ( context.phase == InputActionPhase.Performed )
        {
            
        }
        else if(context.phase == InputActionPhase.Canceled )
        {
            
        }
    }
}