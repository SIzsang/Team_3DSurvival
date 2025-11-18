using _02_Scripts.Narrative;
using _02_Scripts.Narrative.Data;
using UnityEngine;

namespace _02_Scripts.Temp
{
    public class PlayCommonTextSample : MonoBehaviour
    {
        private DialogueManager _dialogueManager;
        [SerializeField] private DialogueData dialogueData;

        public void ShowCommonText()
        {
            if(_dialogueManager == null)_dialogueManager = DialogueManager.Instance;
            _dialogueManager.StartDialogue("abc");
        }


    }
}
