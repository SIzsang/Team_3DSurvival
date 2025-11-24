using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBuildTest : MonoBehaviour
{

    [SerializeField] LayerMask layerMask;

    //test
    [SerializeField] GameObject buildPrefab;

    [Space(5)]
    [SerializeField] private Transform rayStartPoint;


    private GameObject nowSpawnedBuild;
    private Vector3 hitPoint;

    [SerializeField] float buildDistance = 2.0f;
    [SerializeField] Material buildMaterial;
    
    void Start()
    {
        BuildStart();
    }

    public void BuildStart()
    {
        nowSpawnedBuild = Instantiate(buildPrefab);

        Collider[] colliders = nowSpawnedBuild.GetComponentsInChildren<Collider>();
        Renderer[] renderers = nowSpawnedBuild.GetComponentsInChildren<Renderer>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        foreach (Renderer renderer in renderers)
        {
            //renderer.material = buildMaterial;
            Material[] mtls = renderer.materials;
            
            for (int i = 0; i < mtls.Length; i++)
            {
                mtls[i] = buildMaterial;
            }
            renderer.materials = mtls;
        }

    }

    // Update is called once per frame
    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        buildDistance += scroll;


        if (Physics.Linecast(rayStartPoint.position, rayStartPoint.position + (CameraManager.Instance.FlatForward * buildDistance)) == false)
        {
            RaycastHit[] hits = Physics.RaycastAll(rayStartPoint.position + (CameraManager.Instance.FlatForward * buildDistance), Vector3.down, 5.0f, layerMask);
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            
            if (hits.Length > 0)
            {
                if (nowSpawnedBuild != null)
                {
                    nowSpawnedBuild.transform.position = hits[0].point;
                    
                    Vector3 rotDir = nowSpawnedBuild.transform.position - this.transform.position;
                    rotDir = Vector3.ProjectOnPlane(rotDir.normalized, Vector3.up);
                    nowSpawnedBuild.transform.rotation = Quaternion.LookRotation(rotDir,hits[0].normal);
                }
            }
        }

        // if (Physics.Linecast(rayStartPoint.position, hitPoint + (CameraManager.Instance.FlatForward * 1.0f), out RaycastHit hit))
        // {
        //     
        // }
        //rayStartPoint


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(buildPrefab, nowSpawnedBuild.transform.position, nowSpawnedBuild.transform.rotation);
            Debug.Log("vv");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        if(CameraManager.Instance == null) return;
        Gizmos.DrawLine(rayStartPoint.position, rayStartPoint.position + (CameraManager.Instance.FlatForward * buildDistance));
        Gizmos.DrawLine(rayStartPoint.position + (CameraManager.Instance.FlatForward * buildDistance), rayStartPoint.position + (CameraManager.Instance.FlatForward * buildDistance) + Vector3.down * 5.0f);
    }
}
