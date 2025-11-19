using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlots : MonoBehaviour
{
    private Recipe recipe;

    public Button CreativeButton;
    public TextMeshProUGUI recipeInfoText;
    public void Setup(RecipeData data)
    {
        recipe = new Recipe(data);
        recipeInfoText.text = $"{recipe.ResipeName}\n{recipe.RequiredItemDescription}";

        CreativeButton.onClick.RemoveAllListeners();
        CreativeButton.onClick.AddListener(OnClickCreative);
    }

    private void OnClickCreative()
    {
        var inventory = GameManager.Instance.Player.Inventory;
        if (recipe.CanCreative(inventory))
        {
            // 제작
            //CreateItem(inventory);
        }
        else
        {
            // 제작 실패
        }
    }

    // 실제 제작 처리
    //private void CreateItem()
    //{
    //    var inventory = GameManager.Instance.Player.Inventory;
    //    foreach (var ingredient in recipe.Ingredients)
    //    {
    //        Item invItem = inventory.Items.Find(x => x.Name == ingredient.item.name);
    //        invItem.Count -= ingredient.count;
    //    }

    //    // 결과 아이템 지급
    //    inventory.AddItem(recipe.OutputItem);

    //    Debug.Log($"{recipe.ResipeName} 제작 완료!");
    //}
}

