using System;
using System.Collections;
using _02_Scripts.Utils;
using UnityEngine;

namespace _02_Scripts.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private float minutesPerSecond;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private CanvasGroup canvasGroup;

        public bool IsTimePaused { get; private set; }
        public int CurrentDay { get; private set; }
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }

        public event Action<GameTimestamp> OnTimeChanged;
        public event Action<int> OnDayChanged;
        public event Action<GameTimestamp> OnGameStart;
        public void PauseTime() => IsTimePaused = true;
        public void ResumeTime() => IsTimePaused = false;
        private float _timeOfDayInMinutes;


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
            GameStart();
        }

        void Update()
        {
            if (IsTimePaused)
            {
                return;
            }
            _timeOfDayInMinutes += Time.deltaTime * minutesPerSecond;

            if (_timeOfDayInMinutes >= Constants.Game.MinutesInADay)
            {
                AdvanceNextDay();
            }
            UpdateTimeProperties();
        }

        public string GetFormattedTime()
        {
            return $"Day {CurrentDay}, {CurrentHour:D2}:{CurrentMinute:D2}";
            // return $"Day {CurrentDay}, {CurrentHour:D2}";
        }

        public GameTimestamp GetGameTimestamp()
        {
            return new GameTimestamp(CurrentDay, CurrentHour, CurrentMinute);
        }

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

            OnDayChanged?.Invoke(CurrentDay);
        }

        public void SkipToNextDay()
        {
            StartCoroutine(ExecuteWithFade(SkipDayRoutine()));
        }

        public IEnumerator ExecuteWithFade(IEnumerator work)
        {
            PauseTime();
            yield return FadeRoutine(1f);
            yield return work;
            yield return FadeRoutine(0f);
            ResumeTime();
        }

        private void GameStart()
        {
            PauseTime();
            CurrentDay = 1;
            _timeOfDayInMinutes = 0;
            UpdateTimeProperties();
            OnGameStart?.Invoke(GetGameTimestamp());
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
    }
}