using System;
using UnityEngine;
using UnityEngine.UI;
using LevelEntity = CubeSurfer.EcsEntity.Level;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.UI
{
    public class LevelCompletedPresenter : MonoBehaviour
    {
        [SerializeField] private LevelEntity level;
        [SerializeField] private PlayerEntity player;
    
        private MainUiWindow _mainWindow;
        private RewardLabel _rewardLabel;
        private Button _continueButton;

        private void Awake()
        {
            _mainWindow = GetComponentInChildren<MainUiWindow>();
            _rewardLabel = GetComponentInChildren<RewardLabel>();
            _continueButton = GetComponentInChildren<Button>();
            
            _mainWindow.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueGameOnButtonClick);

            player.OnLevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueGameOnButtonClick);
            
            player.OnLevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            _mainWindow.gameObject.SetActive(true);
            
            _rewardLabel.SetValues(level.CompletionReward, player.ScoreMultiplier);
        }
        
        private void ContinueGameOnButtonClick()
        {
            _mainWindow.gameObject.SetActive(false);
        }
    }
}