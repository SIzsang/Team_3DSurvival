using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ItemType
    {
        Equipable,
        Consumable,
        Resource
    }

    public enum ConsumableType
    {
        Hunger,
        Thirst
    }

    [Serializable]
    public class ItemDataConsumable
    {
        public ConsumableType type;
        public float value;
    }

[CreateAssetMenu(fileName = "new Item", menuName = "Scriptable Objects/Item")]
    public class ItemData : ScriptableObject
    {
    [Header("Info")]
    public string displayName; // 이름
    public string description; // 설명
    public ItemType type; // 타입
    public GameObject dropPrefab; // 상호작용시 drop 할 프리팹

    [Header("Stacking")]
    public bool canStack; 
    public int maxStackAmount; // 수량

    [Header("Eating")]
    public bool canEating; // 가공된 음식 = 섭취 가능

    [Header("Attking")]
    public bool canAtk;
    public int atkValue;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables; // 회복 타입, 값

    [Header("Equip")]
    public GameObject equipPrefab; // 장착 프리팹

    public virtual Item NewItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}
