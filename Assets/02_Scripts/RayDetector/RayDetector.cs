using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RayDetector<T> : MonoBehaviour
{
    [SerializeField] protected Transform startTransform;
    [SerializeField] protected float interactionDistance = 1.0f;
    [SerializeField] protected float interactionShapeRange = 1.0f;
    [SerializeField] protected EInteractionDetectorShape rayShape;

    public RaycastHit[] Hits => hits;
    protected RaycastHit[] hits;

    public T CurrentTarget => currentTarget;
    protected T currentTarget;

    public List<T> CurrentTargets => GetAllTarget();

    [Header("Debug")]
    [SerializeField]
    private Color gizmosColor = Color.cyan;

    private void Awake()
    {
        hits = new RaycastHit[] { };

    }

    private void Update()
    {
        switch (rayShape)
        {

            case EInteractionDetectorShape.Line:
                hits = Physics.RaycastAll(startTransform.position, startTransform.forward, interactionDistance);
                break;
            case EInteractionDetectorShape.Sphere:
                hits = Physics.SphereCastAll(startTransform.position, interactionShapeRange, startTransform.forward, interactionDistance);
                break;
            
            case EInteractionDetectorShape.Cube:
                Vector3 halfExtents = new Vector3(interactionShapeRange,interactionShapeRange,interactionShapeRange);
                hits = Physics.BoxCastAll(startTransform.position, halfExtents, startTransform.forward, startTransform.rotation, interactionDistance);
                break;
            
            case EInteractionDetectorShape.Capsule:
                // 나중에....
                
                break;

        }

        if (hits.Length > 0)
        {
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            CheckTarget();
        }
        else
        {
            currentTarget = default(T);
        }

    }


    protected bool CheckTarget()
    {
        foreach (RaycastHit hit in hits)
        {
            T target = hit.collider.GetComponent<T>();
            if (target != null)
            {
                currentTarget = target;
                return true;
            }
        }

        // null 넣으면 오류나네
        // 제너릭에서 default 넣으면 값형이면 0, false 같은게 들어가고 참조형식이면 null 들어간대
        currentTarget = default(T);
        return false;
    }

    protected List<T> GetAllTarget()
    {
        List<T> list = new List<T>();

        foreach (RaycastHit hit in hits)
        {
            T target = hit.collider.GetComponent<T>();
            if (target != null)
            {
                list.Add(target);
            }
        }
        return list;
    }

    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        switch (rayShape)
        {
            case EInteractionDetectorShape.Line:
                Gizmos.DrawLine(startTransform.position, startTransform.position + (startTransform.forward * interactionDistance));
                break;
            case EInteractionDetectorShape.Sphere:
                Gizmos.DrawWireSphere(startTransform.position, interactionShapeRange);
                Gizmos.DrawWireSphere(startTransform.position + (startTransform.forward * interactionDistance), interactionShapeRange);
                break;
                
            case EInteractionDetectorShape.Cube:
                Vector3 halfExtents = new Vector3(interactionShapeRange, interactionShapeRange, interactionShapeRange);
                Gizmos.matrix = Matrix4x4.TRS(startTransform.position + startTransform.forward * (interactionDistance / 2), startTransform.rotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2 + new Vector3(0, 0, interactionDistance));
                break;
        }
    }

#endif
}
