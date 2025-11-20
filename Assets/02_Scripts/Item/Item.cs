using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item
{
    public string Name => name;

    public string DisplayName => displayName;

    public string Description => description;
    public int Count => count;
    public bool CanStack => canStack;
    public int MaxCount => maxCount;
    public bool CanEating => canEating;
    public bool CanAtk => canAtk;
    public int AtkCalue => atkValue;


    private string name;
    private string displayName;
    private string description;

    private int count;
    private bool canAtk;
    private int atkValue;
    private bool canStack;
    private int maxCount;
    private bool canEating;

    public Item(ItemData itemData, int _count = 1)
    {
        name = itemData.name;
        displayName = itemData.displayName;
        description = itemData.description;
        canStack = itemData.canStack;
        canAtk = itemData.canAtk;
        atkValue = itemData.atkValue;
        maxCount = itemData.maxStackAmount;
        canEating = itemData.canEating;
        count = _count;
    }

    public void AddCount(int amount)
    {
        count += amount;
    }
}
