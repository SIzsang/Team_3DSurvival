using _02_Scripts.Quest;
using _02_Scripts.Quest.Context;
using UnityEngine;

namespace _02_Scripts.Temp.Gw
{
    public class TestQuestProcessButton : MonoBehaviour
    {
        private QuestManager _questManager;
        [SerializeField] private ItemData itemData;

        void Start()
        {
            _questManager = QuestManager.Instance;
        }

        public void ProcessQuest()
        {
            Debug.Log("ProcessQuest");
            _questManager.CheckQuestProgress(new QuestProcessContext(QuestType.Craft, itemData));
        }

    }
}