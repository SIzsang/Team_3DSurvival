using _02_Scripts.Quest;
using UnityEngine;

namespace _02_Scripts.Temp.Gw
{
    public class TestQuestButton : MonoBehaviour
    {
        private QuestManager _questManager;

        void Start()
        {
            _questManager = QuestManager.Instance;
        }

        public void AcceptQuest()
        {
            Debug.Log("AcceptQuest");
            // _questManager.SetQuestProgressIfAvailable();
        }


    }
}