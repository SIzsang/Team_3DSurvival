using _02_Scripts.Quest;
using UnityEngine;

namespace _02_Scripts.Temp.Gw
{
    public class TestQuestCompleteButton : MonoBehaviour
    {
        private QuestManager _questManager;

        void Start()
        {
            _questManager = QuestManager.Instance;
        }


        public void ClearQuest()
        {
            Debug.Log("Cleared Quest");
            // _questManager.CheckQuestClear();
        }

    }
}