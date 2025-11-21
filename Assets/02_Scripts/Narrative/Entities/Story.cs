using System.Collections.Generic;
using System.Linq;
using _02_Scripts.Core;
using _02_Scripts.Narrative.Data;

namespace _02_Scripts.Narrative.Entities
{
    public class Story
    {
        public string StoryId;
        // public GameTimestamp StoryTimestamp;
        public readonly List<Dialogue> Dialogues;
        // public TriggerType TriggerType;
        public bool NeedFade;
        // public bool HasBeenPlayed { get; private set; }
        // public void SetPlayed() => HasBeenPlayed = true;

        public Story(StoryData data)
        {
            StoryId = data.StoryId;
            // StoryTimestamp = data.gameTimestamp;
            // TriggerType = data.triggerType;
            Dialogues = data.dialogue.Select(dialogueData => new Dialogue(dialogueData)).ToList();
            // HasBeenPlayed = false;
            NeedFade = data.needFade;
        }

    }
}