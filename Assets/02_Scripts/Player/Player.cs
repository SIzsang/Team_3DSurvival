using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerBehaviour))]
[RequireComponent(typeof(PlayerStatus))]
public class Player : MonoBehaviour, ICombatable
{
    public PlayerInputHandler Input => inputHandler;
    private PlayerInputHandler inputHandler;

    private PlayerBehaviour behaviour;
    public PlayerStatus Status => status;
    private PlayerStatus status;

    private InteractableDetector interactableDetector;
    private CombatableDetector combatableDetector;
    private GatherableDetector gatherableDetector;

    public Inventory Inventory => inventory;
    Inventory inventory;
    public Vector3 Forward => behaviour.Forward;

    public bool IsMoving
    {
        get => isMoving;
        set
        {
            if (isMoving == value) return;
            else
            {
                if (value == false)
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
        behaviour = GetComponent<PlayerBehaviour>();

        interactableDetector = GetComponent<InteractableDetector>();
        combatableDetector = GetComponent<CombatableDetector>();
        gatherableDetector = GetComponent<GatherableDetector>();

        status = GetComponent<PlayerStatus>();

        inputHandler = new PlayerInputHandler();
        inputHandler.Init(this, behaviour);

        inventory = new Inventory();
    }

    private void Update()
    {
        IsMoving = behaviour.IsMoving;
    }


    public void Interaction()
    {
        OnTryInteractAction?.Invoke();
        if (interactableDetector.CurrentTarget != null)
        {
            OnInteractAction?.Invoke();
            interactableDetector.CurrentTarget.OnInteract();
        }
    }

    public void Attack()
    {
        //attack 이 행동 우선순위 높게
        if (status.NowStamina < status.AttackStaminaCost || canAttack == false)
            return;

        OnTryAttackAction?.Invoke();
        status.AddStamina(-status.AttackStaminaCost);
        StartAttackDelayRoutine();
        
        if (combatableDetector.CurrentTarget != null)
        {
            OnAttackAction?.Invoke();

            // 일단 10으로 때려
            // 장비확인 후 때리는 거 넣어야함.
            combatableDetector.CurrentTarget.TakePhysicalDamage(10);

        }
        else if (gatherableDetector.CurrentTarget != null)
        {
            gatherableDetector.CurrentTarget.OnGather();
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        OnHitAction?.Invoke();
        
        status.AddHealth(damage);
        // 맞음
    }

    public void Jump()
    {
        if (behaviour.Jump())
        {
            OnJumpAction?.Invoke();
            behaviour.Jump();
            status.AddStamina(-status.JumpStaminaCost);
        }
    }

    public void Landing()
    {
        OnLandingAction?.Invoke();
    }

    public void StartDash()
    {
        status.StartDash();
    }

    public void StopDash()
    {
        status.StopDash();
        
    }


    public void EatFood()
    {
        
    }

    public void DrinkWater()
    {
        
    }


    public void EquipAx()
    {
        
    }

    public void EquipSword()
    {
        
    }

    public void UnEquip()
    {
        
    }

    private bool canAttack = true;
    private Coroutine attackDelayRoutin;

    void StartAttackDelayRoutine()
    {
        if (attackDelayRoutin != null)
            StopCoroutine(attackDelayRoutin);
        attackDelayRoutin = StartCoroutine(AttackDelayRoutine(status.AttackDealy));
    }

    IEnumerator AttackDelayRoutine(float delay)
    {
        canAttack = false;
        yield return new WaitForSeconds(delay);
        canAttack = true;
        attackDelayRoutin = null;
    }

}
