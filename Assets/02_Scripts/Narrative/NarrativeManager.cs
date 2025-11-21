using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core;
using _02_Scripts.Core.Managers;
using _02_Scripts.Enemy;
using _02_Scripts.Narrative.Data;
using _02_Scripts.Narrative.Entities;
using Core.Managers;
using UnityEngine;

namespace _02_Scripts.Narrative
{
    public class NarrativeManager : MonoBehaviour
    {
        public static NarrativeManager Instance { get; private set; }
        private GameManager _gameManager;
        private DialogueManager _dialogueManager;
        private AudioManager _audioManager;

        [SerializeField] private List<StoryData> stories;
        [SerializeField] private StoryData prologueStoryBeforeFade;
        [SerializeField] private StoryData prologueStoryAfterFade;
        [SerializeField] private StoryData afterFirstBattleStory;
        [SerializeField] private StoryData afterCompleteHouseStory;
        [SerializeField] private NarrativeEnemySpawner enemySpawner;

        private readonly Dictionary<string, Story> _stories = new Dictionary<string, Story>();
        // private readonly Dictionary<GameTimestamp, Story> _storyByInteraction = new Dictionary<GameTimestamp, Story>();
        private readonly HashSet<string> _playedStoryIds = new HashSet<string>();


        private bool _isFirstBattleFinished;
        private bool _isLasttBattleFinished;

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
            _audioManager = AudioManager.Instance;
            Initialize();
        }

        void Initialize()
        {
            foreach (var storyData in stories)
            {
                Story story = new Story(storyData);
                _stories.Add(story.StoryId, story);
                // if (story.TriggerType == TriggerType.Date)
                // {
                // _stories[.gameTimestamp] = story;
                // }
                // else if (story.TriggerType == TriggerType.Interaction)
                // {
                //     _storyByInteraction[storyData.gameTimestamp] = story;
                // }
            }
            StartCoroutine(ShowPrologue());
            enemySpawner.OnBattleFinished += ShowFirstBattleFinish;
            enemySpawner.OnBattleFinished += ShowLastBattleFinish;

        }

        // void OnEnable()
        // {
        //     if (_gameManager == null)
        //     {
        //         _gameManager = GameManager.Instance;
        //     }
        //
        //     if (_dialogueManager == null)
        //     {
        //         _dialogueManager = DialogueManager.Instance;
        //     }
        //     if (_gameManager != null)
        //     {
        //         // _gameManager.OnGameStart += ProgressMainStoryWithFade;
        //         // _gameManager.OnTimeChanged += CheckAndProgressNarrativeByTimeStamp;
        //     }
        // }

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
        public IEnumerator CheckAndProgressNarrative(StoryData storyData)
        {
            InputManager.Instance.OpenUI();
            InputManager.Instance.UseCursor();
            if (!IsNarrativeExists(storyData.StoryId)) yield break;

            Story story = _stories[storyData.StoryId];
            _playedStoryIds.Add(story.StoryId);
            if (_audioManager != null)
            {
                if (storyData.StoryId != prologueStoryBeforeFade.StoryId && storyData.needFade)
                {
                    _audioManager.ClearSfx();
                    _audioManager.PlayBgm(_audioManager.recallBgm);
                    _audioManager.SetSfxAndBgmFix(true);
                }
            }
            if (storyData.needFade || storyData.isEnding)
            {
                bool isEnding = storyData.isEnding;
                yield return StartCoroutine(_gameManager.ExecuteWithFade(ProgressMainStory(storyData),isEnding));
            }
            else
            {
                yield return StartCoroutine(ProgressMainStory(storyData));
            }
            bool isDaytime = _gameManager.IsDaytime();
            _audioManager.SetSfxAndBgmFix(false);
            _gameManager.ChangeBgmByTime(isDaytime);
            if (storyData.nextStory != null)
            {
                yield return CheckAndProgressNarrative(storyData.nextStory);
            }

            if (storyData.isEnding)
            {
                _gameManager.ProcessEnding();
            }
            InputManager.Instance.CloseUI();
            InputManager.Instance.UnuseCursor();
        }

        /// <summary>
        /// 지정된 타임스탬프에 해당하는 스토리가 존재하는지 확인하고, 존재할 경우에만 스토리를 진행합니다.
        /// </summary>
        /// <param name="timestamp">확인 및 진행할 게임의 특정 시간입니다.</param>
        // public void CheckAndProgressNarrativeByTimeStamp(GameTimestamp timestamp)
        // {
            // if (!IsNarrativeExists(TriggerType.Date, timestamp)) return;
            // StartCoroutine(ProgressMainStoryByDate(timestamp));
        // }
        // public void CheckAndProgressNarrative(StoryData story)
        // {
        // if (!IsNarrativeExists(TriggerType.Date, timestamp)) return;
        // StartCoroutine(ProgressMainStoryByDate(timestamp));
        // }

        /// <summary>
        /// 화면 페이드 효과(Fade-in/out)와 함께 메인 스토리를 진행합니다.
        /// </summary>
        /// <param name="gameTimestamp">진행할 스토리의 타임스탬프입니다.</param>
        // public void ProgressMainStoryWithFade(GameTimestamp gameTimestamp)
        // {
            // if (!isMainNarrativeOn) return;
            // StartCoroutine(_gameManager.ExecuteWithFade(ProgressMainStoryByDate(gameTimestamp)));
        // }

        /// <summary>
        /// 특정 타임스탬프에 해당하는 메인 스토리를 실제로 진행하는 핵심 코루틴입니다.
        /// 스토리가 존재하고 아직 플레이되지 않았을 경우에만 대화를 시작하고, 대화가 끝날 때까지 대기합니다.
        /// </summary>
        /// <param name="storyData">재생할 스토리의 고유 타임스탬프입니다.</param>
        /// <returns>코루틴 실행을 위한 IEnumerator를 반환합니다.</returns>
        private IEnumerator ProgressMainStory(StoryData storyData)
        {

            if (_stories.Count == 0)
            {
                Initialize();
            }
            if (_dialogueManager == null)
            {
                _dialogueManager = DialogueManager.Instance;
            }
            if (!_stories.TryGetValue(storyData.StoryId, out Story story))
            {
                yield break;
            }
            // if (story.HasBeenPlayed)
            // {
                // yield break;
            // }
            // story.SetPlayed();
            _dialogueManager.StartDialogue(story);
            yield return new WaitUntil(() => !_dialogueManager.IsDialogueActive);
        }

        private IEnumerator ShowPrologue()
        {
            if (!_gameManager.IsMainNarrativeOn) yield break;

            yield return CheckAndProgressNarrative(prologueStoryBeforeFade);
            yield return CheckAndProgressNarrative(prologueStoryAfterFade);
            yield return enemySpawner.SpawnEnemyBulk();
        }

        private void ShowFirstBattleFinish()
        {
            if(_isFirstBattleFinished) return;
            StartCoroutine(CheckAndProgressNarrative(afterFirstBattleStory));
            _isFirstBattleFinished = true;
        }

        public void ProgressAfterHouseComplete()
        {
            StartCoroutine(CheckAndProgressNarrative(afterCompleteHouseStory));
        }

        private void ShowLastBattleFinish()
        {
            if (!_isFirstBattleFinished) return;

        }

        private bool IsNarrativeExists(string storyId)
        {
            if (_playedStoryIds.Contains(storyId)) return false;
            return _stories.ContainsKey(storyId);
            // if (triggerType == TriggerType.Date)
            // {
                // return _stories.ContainsKey(storyId);
            // }
            // else if(triggerType == TriggerType.Interaction)
            // {
            //     return  _storyByInteraction.ContainsKey(timestamp);
            // }
            // return false;
        }

        // void OnDisable()
        // {
        //     if (_gameManager != null)
        //     {
        //         // _gameManager.OnGameStart -= ProgressMainStoryWithFade;
        //         // _gameManager.OnTimeChanged -= CheckAndProgressNarrativeByTimeStamp;
        //     }
        // }
    }
}