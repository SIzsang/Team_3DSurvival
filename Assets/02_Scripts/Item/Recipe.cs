using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Recipe
{
    public string ResipeName => resipeName;
    public string RequiredItemDescription => requiredItemDecription;
    public ItemData OutputItem => outputItem;
    public int OutputCount => outputCount;
    public List<Ingredient> Ingredients => ingredients;
    public bool IsCreative => isCreative;


    private string resipeName;
    private string requiredItemDecription;

    private ItemData outputItem; // 제작 결과 아이템
    private int outputCount = 1;
    private bool isCreative;


    private List<Ingredient> ingredients = new List<Ingredient>();

    public Recipe(RecipeData recipeData)
    {
        resipeName = recipeData.recipeName;
        requiredItemDecription = recipeData.requiredItemDecription;
        outputItem = recipeData.outputItem;
        outputCount = recipeData.outputCount;
        isCreative = recipeData.isCreative;
        ingredients = recipeData.ingredients;
    }

    public bool CanCreative(Inventory inventory)
    {
        List<Item> items = inventory.Items;

        foreach (Ingredient ingredient in ingredients)
        {
            Item findItem = items.Find(x => x.Name == ingredient.item.name);

            if (findItem == null || findItem.Count < ingredient.count)
            {
                return false;
            }
        }
        return true;
    }
}
