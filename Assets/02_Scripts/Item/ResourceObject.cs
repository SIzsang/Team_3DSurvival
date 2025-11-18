using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using UnityEngine;

public class ResourceObject : MonoBehaviour, ICombatable
{
    public ItemData data;
    public int quantityPerHithit;
    public int capacity;
    Vector3 lastPosition; // 생성 마지막 위치
    
    private void Awake()
    {
        lastPosition = transform.position;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnDaytimeStart += Respown;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDaytimeStart -= Respown;
    }
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}"; // player.~~~
        return str;
    }
    public void TakePhysicalDamage(int damage)
    {
        for (int i = 0; i < quantityPerHithit; i++)
        {
            if (capacity <= 0) break;
            {
                capacity-=1;
                Vector3 randomOffset = new Vector3(
                    Random.Range(-0.5f, 0.5f),// 좌우 
                     0,                       // 높이
                    Random.Range(-0.5f, 0.5f) // 앞뒤
                );

                Vector3 spawnPos = transform.position + randomOffset + Vector3.up;

                Instantiate(data.dropPrefab, spawnPos, Quaternion.identity);
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
