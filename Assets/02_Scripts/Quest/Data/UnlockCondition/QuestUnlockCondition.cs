using UnityEngine;

namespace _02_Scripts.Quest.Data
{
    public abstract class QuestUnlockCondition : ScriptableObject
    {
        public abstract bool IsMet();
    }
}