using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using _02_Scripts.Quest;
using _02_Scripts.Quest.Context;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}"; // player.~~~
        return str;
    }

    public void OnInteract()
    {
        Item item = new Item(data);
        // 플레이어 인벤토리 달면 추가
        GameManager.Instance.Player.Inventory.AddItem(item);
        QuestManager.Instance.CheckQuestProgress(new QuestProcessContext(QuestType.Gather, data));
        Destroy(this.gameObject);
    }
}
