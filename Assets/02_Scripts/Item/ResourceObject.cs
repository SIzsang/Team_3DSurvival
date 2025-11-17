using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using UnityEngine;

public class ResourceObject : MonoBehaviour//, IInteractable
{
    public ItemData giveItem;
    public int quantityPerHithit;
    public int capacity;
    Vector3 lastPosition; // 생성 마지막 위치
    GameManager gameManager;
    

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
    private void Awake()
    {
        lastPosition = transform.position;
    }
    private void OnEnable()
    {
        gameManager.OnDaytimeStart += Respown;
    }

    private void OnDisable()
    {
        gameManager.OnDaytimeStart -= Respown;
    }

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

    public void Respown() // 리스폰
    {
        Vector3 position = lastPosition;
        Instantiate(gameObject, position, Quaternion.identity);
    }
}
