using System;
using Unity.VisualScripting;
using UnityEngine;

[ RequireComponent( typeof( PlayerBehaviour ) ) ]
[ RequireComponent( typeof( PlayerInputHandler ) ) ]
[ RequireComponent( typeof( PlayerStatus ) ) ]
public class Player : MonoBehaviour, ICombatable
{
    private PlayerBehaviour behaviour;
    private PlayerInputHandler inputHandler;

    private InteractableDetector interactableDetector;
    private CombatableDetector combatableDetector;
    private GatherableDetector gatherableDetector;

    public PlayerStatus Status => status;
    private PlayerStatus status;

    public Inventory Inventory => inventory;
    Inventory inventory;
    public Vector3 Forward => behaviour.Forward;

    public bool IsMoving
    {
        get => isMoving;
        set
        {
            if ( isMoving == value ) return;
            else
            {
                if ( value == false )
                {
                    OnIdleAction.Invoke();
                }
                else
                {
                    OnMoveAction.Invoke();
                }

                isMoving = value;
            }
        }
    }

    private bool isMoving;

    public float NowMoveSpeed => behaviour.NowMoveSpeed;

    public event Action OnMoveAction;
    public event Action OnIdleAction;
    public event Action OnJumpAction;
    public event Action OnLandingAction;
    public event Action OnTryInteractAction;
    public event Action OnInteractAction;
    public event Action OnTryAttackAction;
    public event Action OnAttackAction;
    public event Action OnHitAction;


    private void Awake()
    {
        behaviour = GetComponent< PlayerBehaviour >();

        interactableDetector = GetComponent< InteractableDetector >();
        combatableDetector = GetComponent< CombatableDetector >();
        gatherableDetector = GetComponent< GatherableDetector >();

        status = GetComponent< PlayerStatus >();

        inputHandler = new PlayerInputHandler();
        inputHandler.Init( this, behaviour );

        inventory = new Inventory();
    }

    private void Update()
    {
        IsMoving = behaviour.IsMoving;
    }


    public void Interaction()
    {
        OnTryInteractAction?.Invoke();
        if ( interactableDetector.CurrentTarget != null )
        {
            OnInteractAction?.Invoke();
            interactableDetector.CurrentTarget.OnInteract();
        }
    }

    public void Attack()
    {
        OnTryAttackAction?.Invoke();

        if ( combatableDetector.CurrentTarget != null )
        {
            OnAttackAction?.Invoke();
            combatableDetector.CurrentTarget.TakePhysicalDamage( 10 );
            // 일단 10으로 때려
        }

        if ( gatherableDetector.CurrentTarget != null )
        {
            gatherableDetector.CurrentTarget.OnGather();
        }
    }

    public void TakePhysicalDamage( int damage )
    {
        OnHitAction?.Invoke();
        // 맞음
    }

    public void Jump()
    {
        if ( behaviour.Jump() )
        {
            OnJumpAction?.Invoke();
            behaviour.Jump();
            status.AddStamina( -status.JumpStaminaCost );
        }
    }

    public void Landing()
    {
        OnLandingAction?.Invoke();
    }
}