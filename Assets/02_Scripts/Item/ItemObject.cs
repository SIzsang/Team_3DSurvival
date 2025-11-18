using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, ICombatable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}"; // player.~~~
        return str;
    }

    public void OnInteract()
    {
        // 플레이어 인벤토리 달면 추가
        Destroy(this.gameObject);
    }
}
