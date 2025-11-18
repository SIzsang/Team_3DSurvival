using _02_Scripts.Quest.Data;
using _02_Scripts.Utils;

namespace _02_Scripts.Quest.Context
{
    public class QuestProcessContext
    {
        public QuestType QuestType;
        public ItemData TargetItem;

        public QuestProcessContext(QuestType questType, ItemData targetItem)
        {
            QuestType = questType;
            TargetItem = targetItem;
        }

        public QuestProcessContext(QuestType questType)
        {
            QuestType = questType;
            TargetItem = null;
        }
    }
}