using _02_Scripts.Temp;
using UnityEngine;

namespace _02_Scripts.Narrative.Data
{
    [System.Serializable]
    public class DialogueLine
    {
        // public Character Character;
        // public string SpeakerName
        // {
        //     get
        //     {
        //         if (Character != null)
        //             return Character.characterName;
        //         else
        //             return "";
        //     }
        // }
        public string speakerName;
        [TextArea(3,5)]
        public string text;
    }
}