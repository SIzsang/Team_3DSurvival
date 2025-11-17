using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private bool isMainNarrativeOn = false;

        private readonly Dictionary<GameTimestamp, Story> _storyByDate = new Dictionary<GameTimestamp, Story>();
        // private readonly Dictionary<GameTimestamp, Story> _storyByInteraction = new Dictionary<GameTimestamp, Story>();
        private readonly HashSet<string> _playedStoryIds = new HashSet<string>();

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
                if (story.TriggerType == TriggerType.Date)
                {
                    _storyByDate[storyData.gameTimestamp] = story;
                }
                // else if (story.TriggerType == TriggerType.Interaction)
                // {
                //     _storyByInteraction[storyData.gameTimestamp] = story;
                // }

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

        /// <summary>
        /// 지정된 StoryData를 기반으로 스토리를 진행합니다.
        /// 스토리의 고유 ID를 확인하여, 한 번이라도 재생된 스토리는 중복으로 실행되지 않도록 보장합니다.
        /// </summary>
        /// <param name="storyData">재생할 스토리를 정의하는 ScriptableObject 데이터입니다.</param>
        /// <remarks>
        /// 주로 NPC와의 대화, 아이템 조사 등 플레이어의 직접적인 상호작용에 의해 발생하는
        /// 일회성 또는 비정기적 스토리를 처리하는 데 사용됩니다.
        /// 내부적으로는 재생된 스토리의 ID를 HashSet에 기록하여 상태를 관리합니다.
        /// </remarks>
        public void CheckAndProgressNarrative(StoryData storyData)
        {
            Story story = new Story(storyData);
            if (_playedStoryIds.Contains(story.StoryId))
            {
                return;
            }
            _playedStoryIds .Add(story.StoryId);
            _dialogueManager.StartDialogue(story);
        }

        /// <summary>
        /// 지정된 타임스탬프에 해당하는 스토리가 존재하는지 확인하고, 존재할 경우에만 스토리를 진행합니다.
        /// </summary>
        /// <param name="timestamp">확인 및 진행할 게임의 특정 시간입니다.</param>
        public void CheckAndProgressNarrativeByTimeStamp(GameTimestamp timestamp)
        {
            if (!IsNarrativeExists(TriggerType.Date, timestamp)) return;
            StartCoroutine(ProgressMainStoryByDate(timestamp));
        }

        /// <summary>
        /// 화면 페이드 효과(Fade-in/out)와 함께 메인 스토리를 진행합니다.
        /// </summary>
        /// <param name="gameTimestamp">진행할 스토리의 타임스탬프입니다.</param>
        public void ProgressMainStoryWithFade(GameTimestamp gameTimestamp)
        {
            if (!isMainNarrativeOn) return;
            StartCoroutine(_gameManager.ExecuteWithFade(ProgressMainStoryByDate(gameTimestamp)));
        }

        /// <summary>
        /// 특정 타임스탬프에 해당하는 메인 스토리를 실제로 진행하는 핵심 코루틴입니다.
        /// 스토리가 존재하고 아직 플레이되지 않았을 경우에만 대화를 시작하고, 대화가 끝날 때까지 대기합니다.
        /// </summary>
        /// <param name="timestamp">재생할 스토리의 고유 타임스탬프입니다.</param>
        /// <returns>코루틴 실행을 위한 IEnumerator를 반환합니다.</returns>
        public IEnumerator ProgressMainStoryByDate(GameTimestamp timestamp)
        {

            if (_storyByDate.Count == 0)
            {
                Initialize();
            }
            if (_dialogueManager == null)
            {
                _dialogueManager = DialogueManager.Instance;
            }
            if (!_storyByDate.TryGetValue(timestamp, out Story story))
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


        private bool IsNarrativeExists(TriggerType triggerType, GameTimestamp timestamp)
        {
            if (triggerType == TriggerType.Date)
            {
                return _storyByDate.ContainsKey(timestamp);
            }
            // else if(triggerType == TriggerType.Interaction)
            // {
            //     return  _storyByInteraction.ContainsKey(timestamp);
            // }
            return false;
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