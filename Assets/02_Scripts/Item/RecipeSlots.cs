using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using _02_Scripts.Narrative;
using _02_Scripts.Quest;
using _02_Scripts.Quest.Context;
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
        //gameObject.SetActive(false);

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
        string resultMessage = "";
        DialogueManager dialogueManager = DialogueManager.Instance;
        if (recipe.CanCreative(inventory))
        {
            resultMessage = $"{recipe.ResipeName}제작의 성공했습니다.";
            CreateItem();
        }
        else
        {
            resultMessage = "제작의 실패했습니다.";
        }
        dialogueManager.StartDialogue(resultMessage);
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
        QuestManager.Instance.CheckQuestProgress(new QuestProcessContext(QuestType.Craft, recipe.OutputItem));
        Debug.Log($"{recipe.ResipeName} 제작 완료!");
    }


    //public void OnClickCreativeForTest() // 삭제 예정
    //{
    //    OnClickCreative();
    //}

    public void DisableUI()
    {
        CreativeButton.interactable = false;  // 클릭 X
    }

    public void EnableUI()
    {
        CreativeButton.interactable = true;
    }
}

