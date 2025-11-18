using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Player player;
    Rigidbody rb;

    public Vector3 Forward => forward;
    private Vector3 forward;

    public bool CanJump => canJump;
    private bool canJump = true;

    public bool IsJumping => isJumping;
    private bool isJumping = false;

    public bool IsMoving => isMoving;
    private bool isMoving = false;

    public float MoveSpeed =>isMoving ? moveSpeed: 0;
    public float moveSpeed = 0;
    
    public bool isDashing = false;

    [ SerializeField ] private LayerMask groundLayerMask;
    [ SerializeField ] private float playerScale = 0.02f;


    // 데이터 분리되면 그걸로 쓰기
    [ Header( "Player Moving Stat" ) ]
    [ SerializeField ] private float speed;

    [ SerializeField ] private float dashSpeedMultiplier = 1.5f;
    [ SerializeField ] private float jumpForce;
    
    private Vector2 inputDir;
    private Transform cam;

    private void Awake()
    {
        player = GetComponent< Player >();
        rb = GetComponent< Rigidbody >();
        forward = rb.transform.forward;
    }

    private void Start()
    {
        cam = CameraManager.Instance.Cam;
    }

    private void FixedUpdate()
    {
        Move();
    }


    public void SetInputDirection( Vector2 _inputDir )
    {
        inputDir = _inputDir;
    }

    void Move()
    {
        Vector3 camForwardFlat = Vector3.ProjectOnPlane( cam.forward, Vector3.up ).normalized;
        Vector3 right = Vector3.Cross( Vector3.up, camForwardFlat ).normalized;
        Vector3 moveDir = ( camForwardFlat * inputDir.y + right * inputDir.x ).normalized;


        // 스태미나 생기면 따로 speed 데리고 와야하는 거 만들어야함.
        if ( moveDir.magnitude > 0.1f )
        {
            isMoving = true;
            forward = moveDir;
            
            moveSpeed = speed;
            moveSpeed = moveSpeed * ( isDashing ? dashSpeedMultiplier : 1.0f );
            
            Vector3 newPosition = rb.position + forward * ( moveSpeed * Time.deltaTime );
            
            rb.Move( newPosition, Quaternion.LookRotation( forward, Vector3.up ) );
        }
        else
        {
            isMoving = false;
        }
    }


    public bool Jump()
    {
        if ( IsGrounded() )
        {
            isJumping = true;
            rb.AddForce( Vector3.up * jumpForce, ForceMode.Impulse );
            return true;
        }
        return false;
    }

    public void SetDashState( bool _isDashing )
    {
        isDashing = _isDashing;
    }


    // 바닥 체크
    bool IsGrounded()
    {
        Ray[] rays = new Ray[ 4 ]
        {
            new Ray( transform.position + ( transform.forward * playerScale ) + ( transform.up * 0.01f ), Vector3.down ),
            new Ray( transform.position + ( -transform.forward * playerScale ) + ( transform.up * 0.01f ), Vector3.down ),
            new Ray( transform.position + ( transform.right * playerScale ) + ( transform.up * 0.01f ), Vector3.down ),
            new Ray( transform.position + ( -transform.right * playerScale ) + ( transform.up * 0.01f ), Vector3.down )
        };

        for ( int i = 0; i < rays.Length; i++ )
        {
            if ( Physics.Raycast( rays[ i ], 0.1f, groundLayerMask ) )
            {
                if ( isJumping ) isJumping = false;
                return true;
            }
        }

        return false;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine( transform.position + ( transform.forward * playerScale ), transform.position + ( transform.forward * playerScale ) + ( -transform.up * 0.01f ) );
        Gizmos.DrawLine( transform.position + ( -transform.forward * playerScale ), transform.position + ( -transform.forward * playerScale ) + ( -transform.up * 0.01f ) );
        Gizmos.DrawLine( transform.position + ( transform.right * playerScale ), transform.position + ( transform.right * playerScale ) + ( -transform.up * 0.01f ) );
        Gizmos.DrawLine( transform.position + ( -transform.right * playerScale ), transform.position + ( -transform.right * playerScale ) + ( -transform.up * 0.01f ) );
    }
#endif
}