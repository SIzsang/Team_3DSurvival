using System.Collections.Generic;
using _02_Scripts.Narrative.Data;
using _02_Scripts.Quest.Data;
using _02_Scripts.Quest.Data.Consequence;
using _02_Scripts.Quest.Data.UnlockCondition;
using UnityEngine;

namespace _02_Scripts.Quest.Entities
{
    public class QuestEntity
    {
        public QuestData Data { get; private set; }

        private int _currentAmount;
        public QuestState CurrentQuestState;

        public string QuestId => Data.QuestId;
        public QuestType QuestType => Data.type;
        public ItemData TargetItem => Data.targetItem;
        public string Description => Data.description;
        public DialogueData RequestDialogue => Data.requestDialogue;
        public DialogueData ClearDialogue => Data.clearDialogue;
        public QuestType Type => Data.type;
        public List<QuestUnlockCondition> QuestUnlockCondition => Data.questUnlockCondition;
        public List<QuestConsequence> QuestConsequence => Data.questConsequence;
        public bool IsClear => _currentAmount >= Data.requiredAmount;


        public QuestEntity(QuestData data)
        {
            Data = data;
            _currentAmount = 0;
            CurrentQuestState = QuestState.Inactive;
        }
        public void IncreaseProgress(int amount)
        {
            if (CurrentQuestState != QuestState.Progress) return;
            _currentAmount += amount;
            _currentAmount = Mathf.Min(_currentAmount, Data.requiredAmount);
        }
        public void SetState(QuestState newState)
        {
            CurrentQuestState = newState;
        }

        public void AcceptQuest()
        {
            if (CurrentQuestState == QuestState.Available)
            {
                CurrentQuestState = QuestState.Progress;
            }
        }
        public void ExecuteQuestConsequence()
        {
            if (QuestConsequence.Count == 0) return;
            if (CurrentQuestState != QuestState.Progress) return;
            for (int i = 0; i < QuestConsequence.Count; i++)
            {
                QuestConsequence[i].Consequence();
            }
            CurrentQuestState = QuestState.Complete;
        }
    }

    public enum QuestState
    {
        Inactive,Available, Progress, Complete
    }
}