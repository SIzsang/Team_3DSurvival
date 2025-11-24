using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    
    [Header("Camera Option")]
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] private float lookSensitivity = 0.2f;
    [SerializeField] private Transform container;
    [SerializeField] private Transform wallCheckTransform;
    [SerializeField] private Transform camTransform;

    public Transform FollowingTarget => _followingTarget;
    private Transform _followingTarget;
    
    public Transform CamTransform => camTransform;
    public Vector3 Forward => camTransform.forward;
    
    [SerializeField] private float minXLook = -50;
    [SerializeField] private float maxXLook = 30;
    
    [SerializeField] private Vector3 thirdPersonPosition = new Vector3(0, 3.0f, -6.5f);
    
    
    private float camCurRotX;
    private float camCurRotY;
    
    private void Update()
    {
        MoveCamera();
        RotateCamera();
        CheckObstacle();
    }

    public void SetTarget(Transform _target)
    {
        _followingTarget = _target;
        camTransform.localPosition = thirdPersonPosition;
    }

    public void CheckObstacle()
    {
        // if (target == null) return;
        // float wallCheckDistance = thirdPersonPosition.magnitude;
        // RaycastHit[] hits = Physics.RaycastAll(target.position, Vector3.Normalize(wallCheckTransform.position - target.position), wallCheckDistance, obstacleLayer);
        //
        // if (hits.Length > 0)
        // {
        //     Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        //
        //     camTransform.position = hits[0].point; 
        // }
        // else
        // {
        //     container.transform.localPosition = Vector3.zero;
        //     camTransform.localPosition = Vector3.zero;
        // }


    }
    
    public void RotateCamera()
    {
        
        container.transform.localEulerAngles = new Vector3(0, camCurRotY, 0);
        //wallCheckTransform.transform.localEulerAngles = new Vector3(0, camCurRotY, 0);
        
        container.localEulerAngles = new Vector3(-camCurRotX, container.localEulerAngles.y, 0);
        // if (isThirdPerson)
        // {
        //    
        //     wallCheckCotainer.localEulerAngles = new Vector3(container.localEulerAngles.x, container.localEulerAngles.y, 0);
        // }
    }

    public void MoveCamera()
    {
        this.transform.position = _followingTarget.transform.position;
    }
    
    
    public void UpdateLookInput(Vector2 delta)
    {
        camCurRotX += delta.y * lookSensitivity;

        camCurRotX = Mathf.Clamp(camCurRotX, minXLook, maxXLook);

        camCurRotY += delta.x * lookSensitivity;
        //
        // Vector3 forward = Quaternion.Euler(0, camCurRotY, 0) * Vector3.forward;
        // return forward;
    }
    


}
