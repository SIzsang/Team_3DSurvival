using _02_Scripts.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _02_Scripts.Temp
{
    public class Test : MonoBehaviour
    {
        private GameManager _gameManager;

        [SerializeField] TextMeshProUGUI timeText;

        void Start()
        {
            _gameManager = GameManager.Instance;
        }
        void Update()
        {
            if (_gameManager == null)
            {
                _gameManager = GameManager.Instance;
            }
            timeText.text = _gameManager.GetFormattedTime();
        }

        public void GoNextDayClicked()
        {
            Debug.Log($"GoNextDayClicked");
            _gameManager.SkipToNextDay();
        }
    }
}
