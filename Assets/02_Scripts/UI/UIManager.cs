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


        private UIInputHandler Input=>_inputHandler;
        UIInputHandler _inputHandler;
        public GameObject RecipeUI => _recipeUI;
        [SerializeField] private GameObject _recipeUI;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _inputHandler = GetComponent<UIInputHandler>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _player = _gameManager.Player;
            
            _inputHandler.OnTabDownAction += OpenRecipeUI;
            _inputHandler.OnTabUpAction += CloseRecipeUI;

        }


        public void OpenRecipeUI()
        {
            _recipeUI.SetActive(true);
        }

        public void CloseRecipeUI()
        {
            _recipeUI.SetActive(false);
        }
    }
}
