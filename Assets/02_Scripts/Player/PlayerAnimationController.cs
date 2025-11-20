using System;
using Unity.VisualScripting;
using UnityEngine;

[ Serializable ]
public class PlayerAnimationController : MonoBehaviour
{
    [ SerializeField ] private Animator animator;
    [ SerializeField ] private float speedMultiplier = 0.1f;
    private Player player;

    // private Vector3 nowPos;
    // private Vector3 prePos;
    // private float moveSpeed;

    private float nowSpeed;

    public void Awake()
    {
        player = GetComponent< Player >();

        // 캐릭터로 바꿔서 다른 캐릭터들한테도 붙일 수 있을 듯..?
        player.OnMoveAction += OnMove;
        player.OnIdleAction += OnIdle;
        player.OnJumpAction += OnJump;
        player.OnInteractAction += OnInteract;
        player.OnTryAttackAction += OnTryAttackAction;
        player.OnHitAction += OnHit;
        player.OnLandingAction += OnLanding;
    }

    private void Update()
    {
        if ( player.IsMoving )
        {
            
            float walkSpeed =  player.Status.MoveSpeed;
            float runSpeed  = walkSpeed * player.Status.DashMultiplier; // 예: 1.5f
            float blend = Mathf.InverseLerp(walkSpeed, runSpeed, player.NowMoveSpeed);
            
            nowSpeed = Mathf.Lerp( nowSpeed, blend, Time.deltaTime * 2.5f );
            animator.SetFloat( "MoveSpeed", nowSpeed );
        }
        else
        {
            nowSpeed = 0;
        }
    }

    void OnMove()
    {
        animator.SetBool( "IsWalking", true );
    }

    void OnIdle()
    {
        animator.SetBool( "IsWalking", false );
    }

    void OnJump()
    {
        animator.SetTrigger( "Jump" );
        animator.SetBool("IsJumping", true);
    }

    void OnInteract()
    {
        animator.SetTrigger( "Interact" );
    }

    void OnTryAttackAction()
    {
        animator.SetTrigger( "Attack" );
    }

    void OnHit()
    {
        animator.SetTrigger( "Hit" );
    }

    void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
}