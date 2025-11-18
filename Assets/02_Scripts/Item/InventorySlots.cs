using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    public ItemData item;
    public TextMeshProUGUI quantityText;
    public int quantity;

    public void Set()
    {
        quantityText.text = $"{item.displayName} {quantity}개";
    }

    public void Clear()
    {
        item = null;
        quantityText.text = string.Empty;
    }
}
