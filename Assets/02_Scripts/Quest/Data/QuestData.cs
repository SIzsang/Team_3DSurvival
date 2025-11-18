using System.Collections.Generic;
using _02_Scripts.Narrative.Data;
using _02_Scripts.Quest.Data.Consequence;
using UnityEngine;

namespace _02_Scripts.Quest.Data
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Scriptable Objects/Quest")]
    public class QuestData : ScriptableObject
    {
        public string QuestId
        {
            get { return name; }
        }

        public List<QuestUnlockCondition> questUnlockCondition;
        public List<QuestConsequence> questConsequence;
        public QuestType type;
        public ItemData targetItem;
        public int requiredAmount;
        public string description;
        public DialogueData requestDialogue;
        public DialogueData clearDialogue;

    }

    public enum QuestType
    {
        Gather, Craft, Kill
    }
}