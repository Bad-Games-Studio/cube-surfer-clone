using System;
using UnityEngine;
using UnityEngine.UI;
using LevelEntity = CubeSurfer.EcsEntity.Level;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.UI.LevelCompleted
{
    public class LevelCompletedPresenter : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        private MainUiWindow _mainWindow;
        private RewardLabel _rewardLabel;
        private Button _continueButton;

        private GemCounterWindow _gemCounter;

        private void Awake()
        {
            _mainWindow = GetComponentInChildren<MainUiWindow>();
            _rewardLabel = GetComponentInChildren<RewardLabel>();
            _continueButton = GetComponentInChildren<Button>();

            _gemCounter = GetComponentInChildren<GemCounterWindow>();
            
            SetWindowsActive(false);
        }

        private void OnEnable()
        {
            gameManager.OnEntitiesCreated += SubscribeToEntities;
            gameManager.OnEntitiesDeleting += UnsubscribeFromEntities;
            
            _continueButton.onClick.AddListener(ContinueGameOnButtonClick);
        }

        private void OnDisable()
        {
            gameManager.OnEntitiesCreated -= SubscribeToEntities;
            gameManager.OnEntitiesDeleting -= UnsubscribeFromEntities;
            
            _continueButton.onClick.RemoveListener(ContinueGameOnButtonClick);
        }
        
        private void SubscribeToEntities()
        {
            gameManager.Player.OnLevelCompleted += OnLevelCompleted;
        }
        
        private void UnsubscribeFromEntities()
        {
            gameManager.Player.OnLevelCompleted -= OnLevelCompleted;
        }

        private void SetWindowsActive(bool value)
        {
            _mainWindow.gameObject.SetActive(value);
            _gemCounter.gameObject.SetActive(value);
        }
        
        private void OnLevelCompleted()
        {
            SetWindowsActive(true);

            _rewardLabel.SetValues(gameManager.Level.CompletionReward, gameManager.Player.ScoreMultiplier);

            gameManager.AddGems(gameManager.Level.CompletionReward * gameManager.Player.ScoreMultiplier);
            _gemCounter.Amount = gameManager.GemsAmount;
        }
        
        private void ContinueGameOnButtonClick()
        {
            SetWindowsActive(false);
        }
    }
}