using System;
using System.Collections;
using CubeSurfer.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using LevelEntity = CubeSurfer.EcsEntity.Level;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.UI.LevelCompleted
{
    public class LevelCompletedPresenter : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        [Header("Game animation")]
        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private float animationTime;
        [SerializeField] private float postAnimationDelay;
        
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
            gameManager.Player.OnLevelCompleted += ShowWindowOnLevelCompleted;
        }
        
        private void UnsubscribeFromEntities()
        {
            gameManager.Player.OnLevelCompleted -= ShowWindowOnLevelCompleted;
        }

        private void SetWindowsActive(bool value)
        {
            _mainWindow.gameObject.SetActive(value);
            _gemCounter.gameObject.SetActive(value);
        }
        
        private void ShowWindowOnLevelCompleted()
        {
            SetWindowsActive(true);

            _rewardLabel.SetValues(gameManager.Level.CompletionReward, gameManager.Player.ScoreMultiplier);
            _gemCounter.Amount = gameManager.GemsAmount;
            
            gameManager.GemsAmount += gameManager.Level.CompletionReward * gameManager.Player.ScoreMultiplier;
        }
        
        private void ContinueGameOnButtonClick()
        {
            StartCoroutine(AddGems());
        }

        private IEnumerator AddGems()
        {
            var startPosition = GetComponentInChildren<GemAnimation.SourceObject>().transform.position;
            
            var newGem = Instantiate(gemPrefab, startPosition, Quaternion.identity);
            var gemTransform = newGem.GetComponent<RectTransform>();
            gemTransform.SetParent(_mainWindow.transform);

            var endPosition = GetComponentInChildren<GemAnimation.DestinationObject>().transform.position;
            newGem.transform.DOMove(endPosition, animationTime).OnComplete(() =>
            {
                _gemCounter.Amount = gameManager.GemsAmount;
            });

            yield return new WaitForSeconds(postAnimationDelay);
            
            SetWindowsActive(false);
            gameManager.NextLevel();
        }
    }
}