using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> Items => items;
    private List<Item> items;
    public bool IsSwordHave => isSwordHave;
    public bool IsAxHave => isAxHave;

    private bool isSwordHave = false;

    private bool isAxHave = false;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        List<Item> finditems = items.FindAll((i) =>
        {
            if (item.Name == i.Name) return true;
            return false;
        });

        if (finditems.Count > 0)
        {
            if (item.CanStack)
            {
                for (int i = 0; i < finditems.Count; i++)
                {
                    if (finditems[i].Count < finditems[i].MaxCount)
                    {
                        finditems[i].AddCount();
                        return;
                    }

                }
            }
        }
        items.Add(item);
    }

    public void SetSwordHave()
    {
        isSwordHave = true;
    }
    public void SetAxHave()
    {
        isAxHave = true;
    }
}
