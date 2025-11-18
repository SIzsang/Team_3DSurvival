using System;
using _02_Scripts.Core.Managers;
using UnityEngine;

namespace _02_Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        private Player _player;
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

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _player = _gameManager.Player;
        }
    }
}