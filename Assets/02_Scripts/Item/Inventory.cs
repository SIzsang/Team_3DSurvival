using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public List<Item> Items => items;
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }
}
