using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CameraInputHandler inputHandler;
    CameraBehaviour behaviour;

    private Transform FollowingTarget => behaviour?.FollowingTarget;

    private void Awake()
    {  
        inputHandler = GetComponent<CameraInputHandler>();
        behaviour = GetComponent<CameraBehaviour>();
        
        inputHandler.Init();
    }
    public void SetTarget(Transform target)
    {
        
        behaviour?.SetTarget(target);
    }

}
