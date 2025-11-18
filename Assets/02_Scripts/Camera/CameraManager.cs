using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CameraInputHandler inputHandler;
    CameraBehaviour behaviour;

    public Vector3 Forward => behaviour.Forward;
    public Transform Cam => behaviour.CamTransform;


    public static CameraManager Instance => instance;
    private static CameraManager instance;

    private Transform FollowingTarget => behaviour?.FollowingTarget;

    private void Awake()
    {
        if ( instance == null )
        {
            instance = this;
            
            inputHandler = GetComponent<CameraInputHandler>();
            behaviour = GetComponent<CameraBehaviour>();
        
            inputHandler.Init();    
        }
        else if ( instance != null )
        {
            Destroy( this );
            return;
        }
        
    }
    
    public void SetTarget(Transform target)
    {
        behaviour?.SetTarget(target);
    }

}
