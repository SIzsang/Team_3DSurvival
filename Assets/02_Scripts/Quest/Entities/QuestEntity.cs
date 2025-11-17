using _02_Scripts.Narrative.Data;
using _02_Scripts.Quest.Data;

namespace _02_Scripts.Quest.Entities
{
    public class QuestEntity
    {
        public string QuestId;
        public QuestType QuestType;
        public ItemData TargetItem;
        private int _requiredAmount;
        private int _currentAmount;
        public string Description;
        public DialogueData RequestDialogue;
        public DialogueData ClearDialogue;
        public bool IsClear => _currentAmount >= _requiredAmount;
        public bool IsAccept;
        public void Accept() => IsAccept = true;

        public QuestEntity(QuestData data)
        {
            QuestId = data.QuestId;
            QuestType = data.type;
            TargetItem = data.targetItem;
            _requiredAmount = data.requiredAmount;
            _currentAmount = 0;
            Description = data.description;
            RequestDialogue = data.requestDialogue;
            ClearDialogue = data.requestDialogue;
        }

        public void IncreaseProgress(int amount)
        {
            _currentAmount += amount;
        }
    }
}