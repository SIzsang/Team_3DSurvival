using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Narrative;
using Unity.VisualScripting;
using UnityEngine;

public class HouseObject : MonoBehaviour, IInteractable
{
    public GameObject houseObject;

    public void OnInteract()
    {
        houseObject.SetActive(true);
        NarrativeManager.Instance.ProgressAfterHouseComplete();
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        Instantiate(houseObject, housePoint);
    //    }
    //}
}
