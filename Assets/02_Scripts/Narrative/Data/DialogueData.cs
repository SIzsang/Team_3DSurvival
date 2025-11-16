using System.Collections.Generic;
using _02_Scripts.Temp;
using UnityEngine;

namespace _02_Scripts.Narrative.Data
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public DialogueType dialogueType;
        public List<DialogueLine> dialogueLines;
    }
    public enum DialogueType
    {
        Common, Narrative
    }
}