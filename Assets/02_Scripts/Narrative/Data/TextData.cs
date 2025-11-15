using UnityEngine;

namespace _02_Scripts.Narrative.Data
{
    [System.Serializable]
    public class TextData
    {
        [TextArea(3,5)]
        public string text;
    }
}