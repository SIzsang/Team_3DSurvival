using UnityEngine;

namespace _02_Scripts.Quest.Data.Consequence
{
    [CreateAssetMenu(fileName = "New ConsequenceUnlockRecipe", menuName = "Scriptable Objects/Quest/Consequence/UnlockRecipe")]
    public class ConsequenceUnlockRecipe : QuestConsequence
    {
        [SerializeField] RecipeData resipe;
        public override void Consequence()
        {

        }
    }
}