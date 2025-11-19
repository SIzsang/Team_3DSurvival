using System;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Player player;
    Rigidbody rb;

    public Vector3 Forward => forward;
    private Vector3 forward;

    
    private bool jumpDelay = true;

    public bool IsJumping => isJumping;
    private bool isJumping = false;

    public bool IsMoving => isMoving;
    private bool isMoving = false;

    public float NowMoveSpeed => isMoving ? nowMoveSpeed : 0;
    private float nowMoveSpeed = 0;

    bool isDashInput = false;
    private bool isDashing = false;
    
    

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float playerScale = 0.02f;


    // // 데이터 분리되면 그걸로 쓰기
    // [Header("Player Moving Stat")]
    // [SerializeField] private float speed;
    // [SerializeField] private float dashSpeedMultiplier = 1.5f;
    // [SerializeField] private float jumpForce;

    private Vector2 inputDir;
    private Transform cam;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        forward = rb.transform.forward;
    }

    private void Start()
    {
        cam = CameraManager.Instance.Cam;
    }

    private void FixedUpdate()
    {
        Move();

        if (isJumping)
        {
            if (IsGrounded())
            {
                isJumping = false;
                player.Landing();
            }
            
        }
    }


    public void SetInputDirection(Vector2 _inputDir)
    {
        inputDir = _inputDir;
    }

    void Move()
    {
        Vector3 camForwardFlat = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, camForwardFlat).normalized;
        Vector3 moveDir = (camForwardFlat * inputDir.y + right * inputDir.x).normalized;
        
        forward = moveDir;

        if ( IsWall() == false )
        {
            return;
        }

        // 스태미나 생기면 따로 speed 데리고 와야하는 거 만들어야함.
        if (moveDir.magnitude > 0.1f)
        {
            isMoving = true;
            nowMoveSpeed = player.Status.MoveSpeed;

            if (isDashInput && player.Status.NowStamina > 0)
            {
                nowMoveSpeed = nowMoveSpeed * player.Status.DashMultiplier;

                if (isDashing == false)
                {
                    isDashing = true;
                    player.StartDash();
                }
            }
            else
            {
                if (isDashing == true)
                {
                    isDashing = false;
                    player.StopDash();                    
                }
            }

            Vector3 newPosition = rb.position + forward * (nowMoveSpeed * Time.deltaTime);

            rb.MovePosition(newPosition);
            rb.MoveRotation(Quaternion.LookRotation(forward, Vector3.up));
            //rb.Move(newPosition, Quaternion.LookRotation(forward, Vector3.up));
        }
        else
        {
            isMoving = false;
        }
    }


    public bool Jump()
    {
        if ( IsGrounded() && CanJump() ) 
        {
            isJumping = true;
            jumpDelay = false;
            StartCoroutine( JumpDelayRoutine() );
            rb.AddForce(Vector3.up * player.Status.JumpForce, ForceMode.Impulse);
            return true;
        }
        return false;
    }

    public bool CanJump()
    {
        if ( jumpDelay == false ) return false;
        if ( player.Status.NowStamina < player.Status.JumpStaminaCost ) return false;
        return true;
    }

    public void SetDashState(bool _isDashInput)
    {
        isDashInput = _isDashInput;
    }


    public float groundTest = 0.4f;
    // 바닥 체크
    bool IsGrounded()
    {
        if ( jumpDelay == false ) return false;
        
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right * playerScale) + (transform.up * 0.05f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], groundTest, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
    
    public bool IsWall()
    {
        Vector3 topStart = this.transform.position + (transform.up *.5f) +( forward * .5f );
        Vector3 topEnd = topStart + ( forward * 0.1f );
        Vector3 bottomStart = this.transform.position+( forward * .5f );
        Vector3 bottomEnd = bottomStart + ( forward * 0.1f );


        if ( Physics.Linecast( topStart, topEnd, out RaycastHit topWallHit ,wallLayerMask) )
            return false;
        if ( Physics.Linecast( bottomStart, bottomEnd, out RaycastHit bottomWallHit,wallLayerMask ) )
            return false;


        return true;
    }

    IEnumerator JumpDelayRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        jumpDelay = true;   
    }
    
    


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + (transform.forward * playerScale), transform.position + (transform.forward * playerScale) + (-transform.up * 0.01f));
        Gizmos.DrawLine(transform.position + (-transform.forward * playerScale), transform.position + (-transform.forward * playerScale) + (-transform.up * 0.01f));
        Gizmos.DrawLine(transform.position + (transform.right * playerScale), transform.position + (transform.right * playerScale) + (-transform.up * 0.01f));
        Gizmos.DrawLine(transform.position + (-transform.right * playerScale), transform.position + (-transform.right * playerScale) + (-transform.up * 0.01f));
        
        
        Vector3 topStart    = transform.position + (transform.up * 0.5f) + (forward * 0.5f);
        Vector3 topEnd      = topStart + (forward * 0.1f);
        Vector3 bottomStart = transform.position + (forward * 0.5f);
        Vector3 bottomEnd   = bottomStart + (forward * 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topStart, topEnd);
        Gizmos.DrawLine(bottomStart, bottomEnd);
    }
#endif
}
