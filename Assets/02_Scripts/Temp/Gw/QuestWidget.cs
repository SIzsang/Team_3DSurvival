using _02_Scripts.Core.Managers;
using _02_Scripts.Quest;
using TMPro;
using UnityEngine;

namespace _02_Scripts.Temp.Gw
{
    public class QuestWidget : MonoBehaviour
    {
        private QuestManager _questManager;
        [SerializeField] private TextMeshProUGUI questText;
        private readonly string _noneQuestText = "진행중인 퀘스트가 없습니다.";

        void Start()
        {
            _questManager = QuestManager.Instance;
            _questManager.OnQuestComplete += CompleteQuestWidget;
            _questManager.OnQuestAccepted += UpdateQuestWidget;
            questText.text = _noneQuestText;
        }

        private void CompleteQuestWidget()
        {
            questText.text = _noneQuestText;
        }

        private void UpdateQuestWidget(string description = null)
        {
            questText.text = description;
        }

        void OnDestroy()
        {
            _questManager.OnQuestComplete -= CompleteQuestWidget;
            _questManager.OnQuestAccepted -= UpdateQuestWidget;
        }
    }
}