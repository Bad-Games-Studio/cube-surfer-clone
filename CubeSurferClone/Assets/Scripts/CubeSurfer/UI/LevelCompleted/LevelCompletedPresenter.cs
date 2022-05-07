using UnityEngine;
using UnityEngine.UI;
using LevelEntity = CubeSurfer.EcsEntity.Level;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.UI.LevelCompleted
{
    public class LevelCompletedPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerEntity player;
        [SerializeField] private LevelEntity level;
    
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
            _continueButton.onClick.AddListener(ContinueGameOnButtonClick);

            player.OnLevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueGameOnButtonClick);
            
            player.OnLevelCompleted -= OnLevelCompleted;
        }

        private void SetWindowsActive(bool value)
        {
            _mainWindow.gameObject.SetActive(value);
            _gemCounter.gameObject.SetActive(value);
        }
        
        private void OnLevelCompleted()
        {
            SetWindowsActive(true);

            _rewardLabel.SetValues(level.CompletionReward, player.ScoreMultiplier);

            var newGemsValue = _gemCounter.Amount + level.CompletionReward * player.ScoreMultiplier;
            _gemCounter.Amount = newGemsValue;
        }
        
        private void ContinueGameOnButtonClick()
        {
            SetWindowsActive(false);
        }
    }
}