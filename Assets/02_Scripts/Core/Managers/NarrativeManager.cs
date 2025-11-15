using System.Collections;
using UnityEngine;

namespace _02_Scripts.Core.Managers
{
    public class NarrativeManager : MonoBehaviour
    {
        public static NarrativeManager Instance { get; private set; }
        private GameManager _gameManager;
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
            _gameManager.OnDayChanged += ProcessNarrativeByDate;
            _gameManager.OnTimeChanged += ProcessNarrativeByTimeStamp;
        }

        public void ProcessNarrativeByTimeStamp(GameTimestamp timestamp)
        {
            Debug.Log($"시간별 이벤트 발생! {timestamp}");
        }

        public void ProcessNarrativeByDate(int day)
        {
            Debug.Log($"일자별 이벤트 발생! {day}");
        }
        private bool IsNarrativeExistsByDate(int day)
        {
            return false;
        }
        private bool IsNarrativeExistsByTimestamp(GameTimestamp timestamp)
        {
            return false;
        }

        public IEnumerator ProgressMainStoryByDate(GameTimestamp timestamp)
        {

            yield return 0f;
        }
    }
}