using UnityEngine;
using UnityEngine.UI;
using LevelEntity = CubeSurfer.EcsEntity.Level;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.UI.PlayerDeath
{
    public class PlayerDeathPresenter : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        private MainUiWindow _mainWindow;
        private Button _retryButton;

        private void Awake()
        {
            _mainWindow = GetComponentInChildren<MainUiWindow>();
            _retryButton = GetComponentInChildren<Button>();

            _mainWindow.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            gameManager.OnEntitiesCreated += SubscribeToEntities;
            gameManager.OnEntitiesDeleting += UnsubscribeFromEntities;
            
            _retryButton.onClick.AddListener(RetryOnButtonClick);
        }

        private void OnDisable()
        {
            gameManager.OnEntitiesCreated -= SubscribeToEntities;
            gameManager.OnEntitiesDeleting -= UnsubscribeFromEntities;
            
            _retryButton.onClick.RemoveListener(RetryOnButtonClick);
        }

        private void SubscribeToEntities()
        {
            gameManager.Player.OnDied += OnPlayerDeath;
        }
        
        private void UnsubscribeFromEntities()
        {
            gameManager.Player.OnDied -= OnPlayerDeath;
        }

        private void RetryOnButtonClick()
        {
            _mainWindow.gameObject.SetActive(false);
        }

        private void OnPlayerDeath()
        {
            _mainWindow.gameObject.SetActive(true);
        }
    }
}