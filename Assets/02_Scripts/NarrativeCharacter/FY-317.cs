using _02_Scripts.Quest;
using UnityEngine;

namespace _02_Scripts.NarrativeCharacter
{
    public class Fy317 : MonoBehaviour, IInteractable
    {
        private QuestManager _questManager;

        public void OnInteract()
        {
            _questManager.AcceptOrCompleteQuest();
        }
    }
}