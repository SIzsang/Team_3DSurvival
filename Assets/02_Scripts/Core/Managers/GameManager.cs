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
            CurrentDay = 1;
            _timeOfDayInMinutes = 0;
            UpdateTimeProperties();
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
            return new GameTimestamp(CurrentDay, CurrentHour);
        }

        public void AdvanceNextDay()
        {
            CurrentDay++;
            _timeOfDayInMinutes -= Constants.Game.MinutesInADay;
            OnDayChanged?.Invoke(CurrentDay);
        }

        public IEnumerator SkipToNextDayStart()
        {
            PauseTime();
            yield return FadeRoutine(1f);
            yield return FadeRoutine(0f);
            CurrentDay++;
            _timeOfDayInMinutes = 0;
            UpdateTimeProperties();
            ResumeTime();

        }

        private void UpdateTimeProperties()
        {
            int newHour = (int)(_timeOfDayInMinutes / 60);
            int newMinute = (int)(_timeOfDayInMinutes % 60);

            if (newHour != CurrentHour || newMinute != CurrentMinute)
            {
                CurrentHour = newHour;
                CurrentMinute = newMinute;
                OnTimeChanged?.Invoke(GetGameTimestamp());
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