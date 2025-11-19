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
    


    private string resipeName;
    private string requiredItemDecription;

    private ItemData outputItem; // 제작 결과 아이템
    private int outputCount = 1;


    private List<Ingredient> ingredients = new List<Ingredient>();

    public Recipe(RecipeData reipeData)
    {
        resipeName = reipeData.recipeName;
        requiredItemDecription = reipeData.requiredItemDecription;
        outputItem = reipeData.outputItem;
        outputCount = reipeData.outputCount;
        ingredients = reipeData.ingredients;
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
