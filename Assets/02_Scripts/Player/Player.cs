using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputHandler InputHandler => inputHandler;
    private PlayerInputHandler inputHandler;

    public PlayerBehaviour Behaviour => behaviour;
    private PlayerBehaviour behaviour;

    public PlayerAnimationController PlayerAnimator => playerAnimator;
    private PlayerAnimationController playerAnimator;

    public Vector3 Forward => behaviour.Forward;

    private void Awake()
    {
        inputHandler = GetComponent< PlayerInputHandler >();
        behaviour = GetComponent< PlayerBehaviour >();
        playerAnimator = GetComponent< PlayerAnimationController >();

        inputHandler.Init( this );
    }
}