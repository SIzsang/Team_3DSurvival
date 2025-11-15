using System.Collections.Generic;
using _02_Scripts.Temp;
using UnityEngine;

namespace _02_Scripts.Narrative.Data
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public Character character;
        public string SpeakerName
        {
            get
            {
                if (character != null)
                    return character.characterName;
                else
                    return "";
            }
        }
        public DialogueType dialogueType;
        public List<TextData> dialogueLines;
    }

    public enum DialogueType
    {
        Common, Narrative
    }
}