using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _02_Scripts.Core;
using _02_Scripts.Core.Managers;
using _02_Scripts.Narrative.Data;
using _02_Scripts.Narrative.Entities;
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
            Initialize();
        }

        void Initialize()
        {
            foreach (var storyData in stories)
            {
                Story story = new Story(storyData);
                _storyInstances[storyData.gameTimestamp] = story;
            }
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
                _gameManager.OnGameStart += ProgressMainStoryWithFade;
                _gameManager.OnTimeChanged += CheckAndProgressNarrativeByTimeStamp;
            }
        }

        public void CheckAndProgressNarrativeByTimeStamp(GameTimestamp timestamp)
        {
            if (!IsNarrativeExists(timestamp)) return;
            StartCoroutine(ProgressMainStoryByDate(timestamp));
        }

        public void ProgressMainStoryWithFade(GameTimestamp gameTimestamp)
        {
            StartCoroutine(_gameManager.ExecuteWithFade(ProgressMainStoryByDate(gameTimestamp)));
        }

        public IEnumerator ProgressMainStoryByDate(GameTimestamp timestamp)
        {
            if (_storyInstances.Count == 0)
            {
                Initialize();
            }
            if (_dialogueManager == null)
            {
                _dialogueManager = DialogueManager.Instance;
            }
            if (!_storyInstances.TryGetValue(timestamp, out Story story))
            {
                yield break;
            }
            if (story.HasBeenPlayed)
            {
                yield break;
            }
            story.SetPlayed();
            _dialogueManager.StartDialogue(story);
            yield return new WaitUntil(() => !_dialogueManager.IsDialogueActive);
        }


        private bool IsNarrativeExists(GameTimestamp timestamp)
        {
            return _storyInstances.ContainsKey(timestamp);
        }

        void OnDisable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnGameStart -= ProgressMainStoryWithFade;
                _gameManager.OnTimeChanged -= CheckAndProgressNarrativeByTimeStamp;
            }
        }
    }
}