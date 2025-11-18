using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI quantityText;

    private void Update()
    {
        Set();
    }
    public void Set()
    {
        var items = GameManager.Instance.Player.Inventory.Items;
        string itemText = "";
        for (int i  = 0; i < items.Count; i++)
        {
            itemText += $"{items[i].DisplayName} {items[i].Count}개\n";
        }
        quantityText.text = itemText;
    }

}
