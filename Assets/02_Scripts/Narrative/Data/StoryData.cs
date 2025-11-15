using System.Collections.Generic;
using _02_Scripts.Core;
using UnityEngine;

namespace _02_Scripts.Narrative.Data
{
    [CreateAssetMenu(fileName = "New Story", menuName = "Scriptable Objects/Story")]
    public class StoryData : ScriptableObject
    {
        public GameTimestamp gameTimestamp;
        public List<DialogueData> dialogue;
        public TriggerType triggerType;
    }

    public enum TriggerType
    {
        Date, Interaction
    }
}