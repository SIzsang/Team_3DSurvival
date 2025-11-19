using System;
using System.Collections;
using _02_Scripts.Narrative;
using _02_Scripts.Utils;
using Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _02_Scripts.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private AudioManager _audioManager;
        private DialogueManager _dialogueManager;

        [SerializeField] private float minutesPerSecond;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool isMainNarrativeOn = false;
        [SerializeField] private Light directionalLight;

        public bool IsMainNarrativeOn => isMainNarrativeOn;
        public bool IsTimePaused { get; private set; }
        public int CurrentDay { get; private set; }
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }

        public event Action<GameTimestamp> OnTimeChanged;
        public event Action<int> OnDayChanged;
        public event Action<GameTimestamp> OnGameStart;
        public event Action OnNightStart;
        public event Action OnDaytimeStart;
        public void PauseTime() => IsTimePaused = true;
        public void ResumeTime() => IsTimePaused = false;
        private float _timeOfDayInMinutes;
        private bool _daytimeInvoked = false;
        private bool _nightInvoked = false;

        [SerializeField] private Player player;
        public Player Player => player;

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
            _audioManager = AudioManager.Instance;
            _dialogueManager = DialogueManager.Instance;
            //DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            GameStart();
            
        }
        
        

        void Update()
        {
            if (_dialogueManager.IsDialogueActive)
            {
                PauseTime();
            }
            else
            {
                ResumeTime();
            }
            if (IsTimePaused)
            {
                return;
            }
            _timeOfDayInMinutes += Time.deltaTime * minutesPerSecond;
            UpdateTimeProperties();
            CheckDay();
            CheckNight();
            if (_timeOfDayInMinutes >= Constants.Game.MinutesInADay)
            {
                AdvanceNextDay();
            }
        }

        /// <summary>
        /// 현재 게임 시간을 문자열로 변환하여 반환합니다.
        /// 시간과 분은 항상 두 자리로 표시됩니다 (예: 07:05).
        /// </summary>
        /// <returns>형식에 맞게 변환된 시간 문자열입니다.</returns>
        public string GetFormattedTime()
        {
            // return $"Day {CurrentDay}, {CurrentHour:D2}:{CurrentMinute:D2}";
            return $"Day {CurrentDay}, {CurrentHour:D2}시";
        }

        /// <summary>
        /// 현재 게임 시간을 나타내는 GameTimestamp 구조체 인스턴스를 생성하여 반환합니다.
        /// 현재 시점을 데이터로 저장하거나 특정 이벤트 시간과 비교할 때 유용합니다.
        /// </summary>
        /// <returns>현재 날짜, 시간, 분을 담은 새로운 GameTimestamp 인스턴스입니다.</returns>
        public GameTimestamp GetGameTimestamp()
        {
            return new GameTimestamp(CurrentDay, CurrentHour, CurrentMinute);
        }

        /// <summary>
        /// 게임 시간을 다음 날로 진행시킵니다. 내부적인 시간 계산 로직을 처리합니다.
        /// </summary>
        /// <param name="isSkip">
        /// true일 경우, 시간을 다음 날의 시작(00:00)으로 초기화합니다.
        /// false일 경우(기본값), 현재 시각을 유지한 채 날짜만 변경합니다 (예: 1일 02:00 -> 2일 02:00).
        /// </param>
        public void AdvanceNextDay(bool isSkip = false)
        {
            CurrentDay++;
            if (isSkip)
            {
                _timeOfDayInMinutes = 0f;
            }
            else
            {
                _timeOfDayInMinutes -= Constants.Game.MinutesInADay;
            }
            _daytimeInvoked = false;
            _nightInvoked = false;
            OnDayChanged?.Invoke(CurrentDay);
        }

        /// <summary>
        /// 화면 페이드 효과와 함께 다음 날로 스킵하는 과정을 시작합니다.
        /// 이 메소드는 실제 로직을 담은 코루틴을 실행하는 역할을 합니다.
        /// </summary>
        public void SkipToNextDay()
        {
            StartCoroutine(ExecuteWithFade(SkipDayRoutine()));
        }

        /// <summary>
        /// 특정 작업(코루틴)을 화면 페이드-아웃/인 효과와 함께 실행하는 범용 코루틴입니다.
        /// 작업 전후로 시간을 정지하고 재개하여 안정적인 실행을 보장합니다.
        /// </summary>
        /// <param name="work">페이드 효과 중간에 실행할 다른 코루틴입니다.</param>
        /// <returns>코루틴 실행을 위한 IEnumerator를 반환합니다.</returns>
        public IEnumerator ExecuteWithFade(IEnumerator work)
        {
            PauseTime();
            yield return FadeRoutine(1f);
            yield return work;
            yield return FadeRoutine(0f);
            ResumeTime();
        }

        public bool IsDaytime()
        {
            return CurrentHour >= Constants.Game.DaytimeInHour && CurrentHour <= Constants.Game.NightInHour;
        }

        public void ChangeBgmByTime(bool isDaytime)
        {
            if (_audioManager == null) return;
            if (isDaytime)
            {
                directionalLight.color = Color.white;
                _audioManager.PlayBgm(_audioManager.daytimeBgm);
            }
            else
            {
                _audioManager.PlayBgm(_audioManager.nighttimeBgm);
                directionalLight.color = Color.black;
            }
        }

        private void GameStart()
        {
            PauseTime();
            CurrentDay = 1;
            _timeOfDayInMinutes = Constants.Game.DaytimeInHour * 60f;
            UpdateTimeProperties();
            OnGameStart?.Invoke(GetGameTimestamp());
            if(!isMainNarrativeOn)
            {
                StartCoroutine(FadeRoutine(0f));
            }
            ChangeBgmByTime(true);

            ResumeTime();
        }


        private IEnumerator SkipDayRoutine()
        {
            AdvanceNextDay(true);
            UpdateTimeProperties();
            yield return null;
        }

        private void UpdateTimeProperties()
        {
            int newHour = (int)(_timeOfDayInMinutes / 60);
            int newMinute = (int)(_timeOfDayInMinutes % 60);

            if (newHour != CurrentHour)
            {
                CurrentHour = newHour;
                CurrentMinute = newMinute;
                OnTimeChanged?.Invoke(GetGameTimestamp());
            }
            else if (newMinute != CurrentMinute)
            {
                CurrentMinute = newMinute;
            }
        }

        private IEnumerator FadeRoutine(float targetAlpha)
        {
            if (canvasGroup == null) yield break;

            float startAlpha = canvasGroup.alpha;
            canvasGroup.blocksRaycasts = targetAlpha > 0.5f;

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = targetAlpha;
            canvasGroup.blocksRaycasts = targetAlpha > 0.5f;
        }

        private void CheckNight()
        {
            if (_nightInvoked) return;
            if (CurrentHour == Constants.Game.NightInHour)
            {
                ChangeBgmByTime(false);
                OnNightStart?.Invoke();
                _nightInvoked = true;
            }
        }

        private void CheckDay()
        {
            if(_daytimeInvoked) return;
            if (CurrentHour == Constants.Game.DaytimeInHour)
            {
                ChangeBgmByTime(true);
                OnDaytimeStart?.Invoke();
                _daytimeInvoked = true;
            }
        }
    }
}