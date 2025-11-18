using UnityEngine;

public class Player : MonoBehaviour, ICombatable
{
    private PlayerInputHandler inputHandler;
    private PlayerBehaviour behaviour;
    private PlayerAnimationController playerAnimator;
    private InteractionDetector interactionDetector;
    private AttackDetector attackDetector;
    
    public Vector3 Forward => behaviour.Forward;
    

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        behaviour = GetComponent<PlayerBehaviour>();
        playerAnimator = GetComponent<PlayerAnimationController>();
        interactionDetector = GetComponent<InteractionDetector>();
        attackDetector = GetComponent<AttackDetector>();

        inputHandler.Init();
    }


    public void OnInteraction()
    {
        if (interactionDetector.CurrentTarget != null)
            interactionDetector.CurrentTarget.OnInteract();
    }

    public void OnAttack()
    {
        Debug.Log("때림");
        if (attackDetector.CurrentTarget != null)
            // 일단 10으로 때려
            attackDetector.CurrentTarget.TakePhysicalDamage(10);
    }

    public void TakePhysicalDamage( int damage )
    {
        // 맞음
    }
}
