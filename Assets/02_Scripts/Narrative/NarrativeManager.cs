using System.Collections.Generic;
using System.Linq;
using _02_Scripts.Core;
using _02_Scripts.Core.Managers;
using _02_Scripts.Narrative.Data;
using UnityEngine;

namespace _02_Scripts.Narrative
{
    public class NarrativeManager : MonoBehaviour
    {
        public static NarrativeManager Instance { get; private set; }
        private GameManager _gameManager;
        private DialogueManager _dialogueManager;

        [SerializeField]private List<StoryData> stories;
        private readonly Dictionary<GameTimestamp, Story> _storyInstances = new Dictionary<GameTimestamp, Story>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            _gameManager = GameManager.Instance;
            _dialogueManager = DialogueManager.Instance;
        }

        void OnEnable()
        {
            if (_gameManager == null)
            {
                _gameManager = GameManager.Instance;
            }

            if (_dialogueManager == null)
            {
                _dialogueManager = DialogueManager.Instance;
            }
            if (_gameManager != null)
            {
                _gameManager.OnDayChanged += CheckAndProgressNarrativeByDate;
                _gameManager.OnTimeChanged += CheckAndProgressNarrativeByTimeStamp;
            }
        }

        public void CheckAndProgressNarrativeByDate(int day)
        {
            GameTimestamp gameTimestamp = new GameTimestamp(day, 0, 0);
            if (!IsNarrativeExists(gameTimestamp)) return;
            ProgressMainStoryByDate(gameTimestamp);
        }

        public void CheckAndProgressNarrativeByTimeStamp(GameTimestamp timestamp)
        {
            if (!IsNarrativeExists(timestamp)) return;
            ProgressMainStoryByDate(timestamp);
        }

        public void ProgressMainStoryByDate(GameTimestamp timestamp)
        {
            if(_dialogueManager == null) _dialogueManager = DialogueManager.Instance;

            if (!_storyInstances.TryGetValue(timestamp, out Story story))
            {
                StoryData storyData = stories.FirstOrDefault(s => s.gameTimestamp.Equals(timestamp));
                if (storyData == null)
                {
                    return;
                }

                story = new Story(storyData);
                _storyInstances[timestamp] = story;
            }

            if (story.HasBeenPlayed)
            {
                return;
            }
            story.SetPlayed();
            _dialogueManager.StartDialogue(story);
        }

        private bool IsNarrativeExists(GameTimestamp timestamp)
        {
            return stories.Any(story => story.gameTimestamp.Equals(timestamp));
        }
        void OnDisable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnDayChanged -= CheckAndProgressNarrativeByDate;
                _gameManager.OnTimeChanged -= CheckAndProgressNarrativeByTimeStamp;
            }
        }
    }
}