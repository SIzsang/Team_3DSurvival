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
        public bool CanStack; 
        public int maxStackAmount; // 수량

        [Header("Attking")]
        public bool canAtk;
        public int AtkValue;

        [Header("Consumable")]
        public ItemDataConsumable[] consumables; // 회복 타입, 값

        [Header("Equip")]
        public GameObject equipPrefab; // 장착 프리팹
    }
