using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class RayDetector< T > : MonoBehaviour
{
    [ SerializeField ] protected Transform startTransform;
    [ SerializeField ] protected float interactionDistance = 1.0f;
    [ SerializeField ] protected float interactionShapeRange = 1.0f;
    [ SerializeField ] protected EInteractionDetectorShape rayShape;
    private List<RaycastHit> hitsList = new List<RaycastHit>();
    

    public List< T > CurrentTargets
    {
        get
        {
            UpdateTarget();
            List< T > result = GetAllTarget();
            return result;
        }
    }

    public T CurrentTarget
    {
        get
        {
            UpdateTarget();
            return currentTarget;
        }
    }

    protected T currentTarget;


    [ Header( "Debug" ) ]
    [ SerializeField ]
    private Color gizmosColor = Color.cyan;

    private void Awake()
    {
        hitsList = new List<RaycastHit>();
    }

    protected void UpdateTarget()
    {
        RaycastHit[] hits = new RaycastHit[] {}; 
        
        switch ( rayShape )
        {
            case EInteractionDetectorShape.Line :
                hits = Physics.RaycastAll( startTransform.position, startTransform.forward, interactionDistance );
                break;
            case EInteractionDetectorShape.Sphere :
                hits = Physics.SphereCastAll( startTransform.position, interactionShapeRange, startTransform.forward, interactionDistance );
                break;

            case EInteractionDetectorShape.Cube :
                Vector3 halfExtents = new Vector3( interactionShapeRange, interactionShapeRange, interactionShapeRange );
                hits = Physics.BoxCastAll( startTransform.position, halfExtents, startTransform.forward, startTransform.rotation, interactionDistance );
                break;

            case EInteractionDetectorShape.Capsule :
                // 나중에....

                break;
        }

        if ( hits.Length > 0 )
        {
            List< RaycastHit > hitsList = hits.ToList();
            
            // 대상에 RayDetector 를 가지고있는 본인도 들어갈 수 있으니 제거
            hitsList.RemoveAll( ( h ) =>
            {
                if ( h.collider.gameObject == this.gameObject ) return true;
                return false;
            } );
            
            hitsList.Sort( ( a, b ) => a.distance.CompareTo( b.distance ) );

            foreach ( RaycastHit hit in hitsList )
            {
                T target = hit.collider.GetComponent< T >();
                if ( target != null )
                {
                    currentTarget = target;
                    return;
                }
            }
        }

        currentTarget = default( T );
    }


    protected List< T > GetAllTarget()
    {
        List< T > list = new List< T >();

        foreach ( RaycastHit hit in hitsList )
        {
            T target = hit.collider.GetComponent< T >();
            if ( target != null )
            {
                list.Add( target );
            }
        }
        return list;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        switch ( rayShape )
        {
            case EInteractionDetectorShape.Line :
                Gizmos.DrawLine( startTransform.position, startTransform.position + ( startTransform.forward * interactionDistance ) );
                break;
            case EInteractionDetectorShape.Sphere :
                Gizmos.DrawWireSphere( startTransform.position, interactionShapeRange );
                Gizmos.DrawWireSphere( startTransform.position + ( startTransform.forward * interactionDistance ), interactionShapeRange );
                break;

            case EInteractionDetectorShape.Cube :
                Vector3 halfExtents = new Vector3( interactionShapeRange, interactionShapeRange, interactionShapeRange );
                Gizmos.matrix = Matrix4x4.TRS( startTransform.position + startTransform.forward * ( interactionDistance / 2 ), startTransform.rotation, Vector3.one );
                Gizmos.DrawWireCube( Vector3.zero, halfExtents * 2 + new Vector3( 0, 0, interactionDistance ) );
                break;
        }
    }

#endif
}