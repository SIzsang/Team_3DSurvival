using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour//, IInteractable
{
    public ItemData giveItem;
    public int quantityPerHithit;
    public int capacity;

    //public float testTime;
    //private void Update()
    //{
    //    testTime = Time.deltaTime;
    //    if (testTime > 0)
    //    {
    //        OnInteract();
    //        testTime = 0;
    //    }
    //}

    public void OnInteract(Vector3 hitPoint, Vector3 hitNomal)
    {
        for (int i = 0; i < quantityPerHithit; i++)
        {
            if (capacity <= 0) break;
            {
                capacity-=1;
                Instantiate(giveItem.dropPrefab, Vector3.up + hitPoint, Quaternion.LookRotation(hitNomal, Vector3.up));
            }
        }
        Destroy(this.gameObject);
    }
}
