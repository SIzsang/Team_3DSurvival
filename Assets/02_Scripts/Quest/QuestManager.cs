using System;
using System.Collections.Generic;
using _02_Scripts.Narrative;
using _02_Scripts.Narrative.Data;
using _02_Scripts.Quest.Context;
using _02_Scripts.Quest.Data;
using _02_Scripts.Quest.Data.UnlockCondition;
using _02_Scripts.Quest.Entities;
using UnityEngine;

namespace _02_Scripts.Quest
{
    public class QuestManager : MonoBehaviour
    {

        public static QuestManager Instance { get; private set; }

        private DialogueManager _dialogueManager;

        [SerializeField]private List<QuestData> quests = new List<QuestData>();
        [SerializeField]private DialogueData notExistentQuestDialogue;

        private Dictionary<string, QuestEntity> _questDictionary = new Dictionary<string, QuestEntity>();
        private HashSet<string> _clearQuests = new HashSet<string>();
        private List<QuestUnlockCondition> _questUnlockConditions = new List<QuestUnlockCondition>();
        private QuestEntity _currentQuest;

        public event Action<string> OnQuestAccepted;
        public event Action OnQuestComplete;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            _dialogueManager = DialogueManager.Instance;
            foreach (var questData in quests)
            {
                _questDictionary.Add(questData.QuestId, new QuestEntity(questData));
            }

            UpdateAllQuestAvailability();
        }

        public void AcceptOrCompleteQuest()
        {
            if (_currentQuest == null)
            {
                SetQuestProgressIfAvailable();
            }
            else
            {
                CheckQuestClear();
            }
        }

        public void CheckQuestProgress(QuestProcessContext context)
        {
            if (_currentQuest == null) return;
            if (context == null) return;
            switch (context.QuestType)
            {
                case QuestType.Kill :
                    _currentQuest.IncreaseProgress();
                    break;
                case QuestType.Gather :
                case QuestType.Craft :
                    if(context.TargetItem == null) return;
                    if (_currentQuest.TargetItem.name != context.TargetItem.name) return;
                    _currentQuest.IncreaseProgress();
                    break;
            }
        }

        public bool IsQuestComplete(string questId)
        {
            return _clearQuests.Contains(questId);
        }
        private void SetQuestProgressIfAvailable()
        {
            if (_currentQuest != null)
            {
                _dialogueManager.StartDialogue(_currentQuest.RequestDialogue);
                return;
            }
            _currentQuest = SetQuestProgress();
            if (_currentQuest == null)
            {
                _dialogueManager.StartDialogue(notExistentQuestDialogue);
            }
            else
            {
                _currentQuest.AcceptQuest();
                OnQuestAccepted?.Invoke(_currentQuest.Description);
                _dialogueManager.StartDialogue(_currentQuest.RequestDialogue);
            }
        }

        private void CheckQuestClear()
        {
            if (_currentQuest == null) return;
            if (_currentQuest.IsClear)
            {
                AddQuestCleared(_currentQuest);
                OnQuestComplete?.Invoke();
            }
        }

        private void AddQuestCleared(QuestEntity quest)
        {
            _clearQuests.Add(quest.QuestId);
            _dialogueManager.StartDialogue(quest.ClearDialogue);
            _currentQuest.ExecuteQuestConsequence();
            _currentQuest = null;
            UpdateAllQuestAvailability();
        }

        private QuestEntity SetQuestProgress()
        {
            foreach (var questEntity in _questDictionary.Values)
            {
                if (questEntity.CurrentQuestState == QuestState.Available)
                {
                    return questEntity;
                }
            }
            return null;
        }
        private void UpdateAllQuestAvailability()
        {
            foreach (var questEntity in _questDictionary.Values)
            {
                if (questEntity.CurrentQuestState != QuestState.Inactive) continue;
                if (UnlockConditionsMet(questEntity))
                {
                    questEntity.SetState(QuestState.Available);
                }
            }
        }

        private bool UnlockConditionsMet(QuestEntity questEntity)
        {
            if (questEntity.Data.questUnlockCondition == null || questEntity.Data.questUnlockCondition.Count == 0)
            {
                return true;
            }

            foreach (var condition in questEntity.Data.questUnlockCondition)
            {
                if (condition is PrerequisitesQuest clearCondition)
                {
                    if (!clearCondition.IsMet())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}