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

        
        UIInputHandler _inputHandler;
        public RecipeUI RecipeUI => _recipeUI;
        [SerializeField] private RecipeUI _recipeUI;

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
            DontDestroyOnLoad(gameObject);
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
            _recipeUI.gameObject.SetActive(true);
        }

        public void CloseRecipeUI()
        {
            _recipeUI.gameObject.SetActive(false);
        }
    }
}
