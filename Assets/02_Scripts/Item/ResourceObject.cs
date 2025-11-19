using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using Core.Managers;
using UnityEngine;

public class ResourceObject : MonoBehaviour, IGatherable
{
    public ItemData data;
    public int quantityPerHithit;
    public int capacity;
    Vector3 lastPosition; // 생성 마지막 위치

    private void Awake()
    {
        lastPosition = transform.position;
    }
    private void Start()
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
    public void OnGather()
    {
        if (capacity > 0)
        {
            capacity -= 1;
            Vector3 randomOffset = new Vector3(
                Random.Range(-0.5f, 0.5f),// 좌우 
                    0,                       // 높이
                Random.Range(-0.5f, 0.5f) // 앞뒤
            );
            Vector3 spawnPos = transform.position + randomOffset + Vector3.up;
            Instantiate(data.dropPrefab, spawnPos, Quaternion.identity);
            AudioManager audioManager = AudioManager.Instance;
            switch (data.name)
            {
                case "Item_Water" : audioManager.PlaySfx(audioManager.getWater);
                    break;
                case "Item_Rock"  : audioManager.PlaySfx(audioManager.getStone);
                    break;
                case "Item_Wood"  : audioManager.PlaySfx(audioManager.getWood);
                    break;
            }
        }
        else if (capacity <= 0)
        {
            this.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
        }

    }
    public void Respown() // 리스폰
    {
        Vector3 position = lastPosition;
        // Instantiate(gameObject, position, Quaternion.identity);
        this.gameObject.GetComponentInChildren<Renderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
    }

}
