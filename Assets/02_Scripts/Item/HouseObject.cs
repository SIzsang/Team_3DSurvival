using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObject : MonoBehaviour, IInteractable
{
    public ItemData houseData;
    public void OnInteract()
    {
        Instantiate(houseData.dropPrefab /* 생성 위치 */ );
    }
}
