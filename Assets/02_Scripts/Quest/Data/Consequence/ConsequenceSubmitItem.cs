using UnityEngine;

namespace _02_Scripts.Quest.Data.Consequence
{
    [CreateAssetMenu(fileName = "New ConsequenceSubmitItem", menuName = "Scriptable Objects/Quest/Consequence/SubmitItem")]
    public class ConsequenceSubmitItem : QuestConsequence
    {
        [SerializeField] ItemData itemData;
        public override void Consequence()
        {

        }
    }
}