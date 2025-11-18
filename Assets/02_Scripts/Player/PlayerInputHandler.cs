using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    private Player player;
    private InputBinder inputBinder;
    private PlayerBehaviour behaviour;
    
    // behaviour 말고 Player 만 가지고 있어도 될 
    public void Init(Player _player,  PlayerBehaviour _behaviour)
    {
        player =  _player;
        behaviour = _behaviour;
        BindInputs();
    }

    void BindInputs()
    {
        inputBinder = InputManager.Instance?.GetInputEventBinder( EInputActionAssetName.Player );

        if (inputBinder != null)
        {
            inputBinder.BindInputEvent(  EPlayerInputActionName.Move , OnMove );
            inputBinder.BindInputEvent(EPlayerInputActionName.Jump, OnJump  );
            inputBinder.BindInputEvent(EPlayerInputActionName.Dash, OnDash );
            
            inputBinder.BindInputEvent(EPlayerInputActionName.Interaction, OnInteraction );
            inputBinder.BindInputEvent(EPlayerInputActionName.Attack,OnAttack);
            
            inputBinder.BindInputEvent(EPlayerInputActionName.Num1, OnNum1 );
            inputBinder.BindInputEvent(EPlayerInputActionName.Num2, OnNum2 );
            inputBinder.BindInputEvent(EPlayerInputActionName.Num3, OnNum3 );
            inputBinder.BindInputEvent(EPlayerInputActionName.Num4, OnNum4 );
        }
        
    }

    void OnMove( InputAction.CallbackContext context )
    {
        
        //behaviour.SetInputDirection( context.ReadValue< Vector2 >() );
    }

    void OnJump( InputAction.CallbackContext context )
    {
        if (context.phase == InputActionPhase.Started)
        {
            player.Jump();
        }
    }

    // 채집도 같이 해야할 듯?
    void OnAttack( InputAction.CallbackContext context )
    {
        if ( context.phase == InputActionPhase.Started )
        {
            
            // 애니메이션 타이밍에 맞게 때리려면
            // 공격 애니메이션 재생?
            
            
            player.Attack();
        }
    }
    
    void OnInteraction( InputAction.CallbackContext context )
    {
        if ( context.phase == InputActionPhase.Started )
        {
            // InteractionDetector
            
            player.Interaction();

        }
            
    }


    void OnDash( InputAction.CallbackContext context )
    {
        if ( context.phase == InputActionPhase.Performed )
        {
            behaviour.SetDashState(true);
        }
        else if(context.phase == InputActionPhase.Canceled )
        {
            behaviour.SetDashState( false );
        }
    }


    void OnNum1(InputAction.CallbackContext context)
    {
        
    }

    void OnNum2(InputAction.CallbackContext context)
    {
        
    }

    void OnNum3(InputAction.CallbackContext context)
    {
        
    }

    void OnNum4(InputAction.CallbackContext context)
    {
        
    }
    
    
}