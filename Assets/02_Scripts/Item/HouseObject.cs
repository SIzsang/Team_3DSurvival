using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using Unity.VisualScripting;
using UnityEngine;

public class HouseObject : MonoBehaviour, IInteractable
{
    private Recipe recipe;
    public GameObject houseObject;

    public void OnInteract()
    {
        var inventory = GameManager.Instance.Player.Inventory;
        foreach (var ingredient in recipe.Ingredients)
        {
            Item invenItem = inventory.Items.Find(x => x.Name == ingredient.item.name);

            if (invenItem == null || invenItem.Count < ingredient.count)
            {
                Debug.Log("재료가 부족합니다.");
                return;
            }
        }

        // 3. 재료 감소
        foreach (var ingredient in recipe.Ingredients)
        {
            Item invenItem = inventory.Items.Find(x => x.Name == ingredient.item.name);
            invenItem.AddCount(-ingredient.count);
        }

        // 결과 아이템 지급
        Item item = new Item(recipe.OutputItem);
        houseObject.SetActive(true);
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        Instantiate(houseObject, housePoint);
    //    }
    //}
}
