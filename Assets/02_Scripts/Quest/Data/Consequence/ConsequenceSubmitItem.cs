using UnityEngine;

namespace _02_Scripts.Quest.Data.Consequence
{
    [CreateAssetMenu(fileName = "New ConsequenceSubmitItem", menuName = "Scriptable Objects/Quest/Consequence/SubmitItem")]
    public class ConsequenceSubmitItem : QuestConsequence
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private int amount;
        public override void Consequence()
        {

        }
    }
}