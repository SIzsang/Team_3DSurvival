using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseObject : MonoBehaviour, IInteractable
{
    public ItemData houseData;

    public GameObject houseObject;

    public Transform housePoint;

    public void OnInteract()
    {
        Instantiate(houseObject, housePoint);
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        Instantiate(houseObject, housePoint);
    //    }
    //}
}
