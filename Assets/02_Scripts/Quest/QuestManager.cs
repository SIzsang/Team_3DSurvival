using System.Collections.Generic;
using _02_Scripts.Narrative;
using _02_Scripts.Quest.Data;
using _02_Scripts.Quest.Entities;
using UnityEngine;

namespace _02_Scripts.Quest
{
    public class QuestManager : MonoBehaviour
    {

        public static QuestManager Instance { get; private set; }
        private DialogueManager _dialogueManager;
        private List<QuestData> _quests = new List<QuestData>();
        private Dictionary<string, QuestEntity> _questDictionary = new Dictionary<string, QuestEntity>();


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
        }



    }
}