using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Ingredient
{
    public ItemData item;   // 필요 재료 아이템
    public int count;       // 필요 수량
}

[CreateAssetMenu(fileName = "new Recipe", menuName = "Scriptable Objects/Resipe")]
public class RecipeData : ScriptableObject
{
    [Header("UI 출력")]
    public string recipeName; // 레시피 이름
    // public string description; // 설명, 요구 재료
    public string requiredItemDecription;

    [Header("출력")]
    public bool isCreative;
    public ItemData outputItem; // 제작 결과 아이템
    public int outputCount = 1;

    [Header("재료")]
    public List<Ingredient> ingredients = new List<Ingredient>();

}
