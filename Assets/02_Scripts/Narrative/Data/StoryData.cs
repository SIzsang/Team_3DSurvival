using System.Collections.Generic;
using UnityEngine;

namespace _02_Scripts.Narrative.Data
{
    [CreateAssetMenu(fileName = "New Story", menuName = "Scriptable Objects/Story")]
    public class StoryData : ScriptableObject
    {
        public int date;
        public List<DialogueData> dialogue;
        public TriggerType triggerType;
    }

    public enum TriggerType
    {
        Date, Interaction
    }
}