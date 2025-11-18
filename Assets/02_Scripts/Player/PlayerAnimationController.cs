using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerAnimationController :MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Player player;

    private Vector3 nowPos;
    private Vector3 prePos;
    private float moveSpeed;
    
    public void Awake()
    {  
        player = GetComponent<Player>();

        player.OnMoveAction += OnMove;
        player.OnIdleAction += OnIdle;
        player.OnJumpAction += OnJump;
        player.OnInteractAction += OnInteract;
        player.OnTryAttackAction += OnTryAttackAction;
        player.OnHitAction += OnHit;
    }


    void OnMove()
    {
        animator.SetBool("IsWalking", true);
    }

    void OnIdle()
    {
        animator.SetBool("IsWalking", false);
    }

    void OnJump()
    {
        animator.SetBool("IsJumping", true);
    }

    void OnInteract()
    {
        animator.SetTrigger("Interact");
    }

    void OnTryAttackAction()
    {
        animator.SetTrigger("Attack");
    }

    void OnHit()
    {
        animator.SetTrigger("Hit");
    }
    
}
