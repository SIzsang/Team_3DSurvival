using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name => name;

    public string DisplayName => displayName;

    public string Description => description;
    public int Count => count;
    public bool CanStack => canStack;
    public int MaxCount => maxCount;

    private string name;
    private string displayName;
    private string description;

    private int count;
    private bool canStack;
    private int maxCount;

    public Item(ItemData itemData, int _count = 1)
    {
        name = itemData.name;
        displayName = itemData.displayName;
        description = itemData.description;
        canStack = itemData.canStack;
        maxCount = itemData.maxStackAmount;
        count = _count;
    }

    public void AddCount()
    {
        count++;
    }
}
