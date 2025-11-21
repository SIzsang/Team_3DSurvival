using UnityEngine;

namespace _02_Scripts.Quest.Data.UnlockCondition
{
    [CreateAssetMenu(fileName = "New PrerequisitesQuest", menuName = "Scriptable Objects/Quest/UnlockCondition/PrerequisitesQuest")]
    public class PrerequisitesQuest : QuestUnlockCondition
    {
        [SerializeField]private QuestData questData;

        public override bool IsMet()
        {
            if(questData == null) return true;
            return QuestManager.Instance.IsQuestComplete(questData.QuestId);
        }
    }
}