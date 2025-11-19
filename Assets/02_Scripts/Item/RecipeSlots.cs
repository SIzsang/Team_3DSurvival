using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlots : MonoBehaviour
{
    private Recipe recipe;
    [SerializeField] private RecipeData recipeData;    
    

    public Button CreativeButton;
    public TextMeshProUGUI recipeInfoText;

    private void Awake()
    {
        Setup(recipeData);
        gameObject.SetActive(false);

    }
    public void Setup(RecipeData data)
    {
        recipe = new Recipe(data);
        recipeInfoText.text = $"{recipe.ResipeName}\n{recipe.RequiredItemDescription}";

        CreativeButton.onClick.RemoveAllListeners();
        CreativeButton.onClick.AddListener(OnClickCreative);
    }


    public void OnClickCreative()
    {
        var inventory = GameManager.Instance.Player.Inventory;
        if (recipe.CanCreative(inventory))
        {
            
            // 제작
            CreateItem();
            Debug.Log("성공 눌렸어");
        }
        else
        {
            Debug.Log("실패 눌렸어");
            // 제작 실패
        }
    }


    public void CreateItem()
    {
        var inventory = GameManager.Instance.Player.Inventory;
        foreach (var ingredient in recipe.Ingredients)
        {
            Item invenItem = inventory.Items.Find(x => x.Name == ingredient.item.name);
            invenItem.AddCount(-ingredient.count);
        }

        // 결과 아이템 지급
        Item item = new Item(recipe.OutputItem);
        inventory.AddItem(item);

        Debug.Log($"{recipe.ResipeName} 제작 완료!");
    }


    public void OnClickCreativeForTest() // 삭제 예정
    {
        OnClickCreative();
    }

    public void DisableUI()
    {
        CreativeButton.interactable = false;  // 클릭 X
    }

    public void EnableUI()
    {
        CreativeButton.interactable = true;
    }
}

