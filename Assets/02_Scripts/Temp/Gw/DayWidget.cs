using _02_Scripts.Core.Managers;
using TMPro;
using UnityEngine;

namespace _02_Scripts.Temp.Gw
{
    public class DayWidget : MonoBehaviour
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

    }
}
