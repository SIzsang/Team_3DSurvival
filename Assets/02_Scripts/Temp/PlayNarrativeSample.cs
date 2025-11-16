using _02_Scripts.Narrative;
using _02_Scripts.Narrative.Data;
using UnityEngine;

namespace _02_Scripts.Temp
{
    public class PlayNarrativeSample : MonoBehaviour
    {
        private NarrativeManager _narrativeManager;
        [SerializeField] private StoryData storyData;

        public void ShowNarrative()
        {
            if(_narrativeManager == null)_narrativeManager = NarrativeManager.Instance;
            _narrativeManager.CheckAndProgressNarrative(storyData);
        }
    }
}
