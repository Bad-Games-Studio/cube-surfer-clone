using UnityEngine;
using UnityEngine.UI;
using LevelEntity = CubeSurfer.EcsEntity.Level;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.UI.PlayerDeath
{
    public class PlayerDeathPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerEntity player;
        [SerializeField] private LevelEntity level;
        
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
            _retryButton.onClick.AddListener(RetryOnButtonClick);

            player.OnDied += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(RetryOnButtonClick);
            
            player.OnDied -= OnPlayerDeath;
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