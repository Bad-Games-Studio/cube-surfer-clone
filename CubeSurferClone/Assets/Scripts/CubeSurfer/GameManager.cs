using System;
using CubeSurfer.GameSave;
using CubeSurfer.LevelGeneration.Presets;
using CubeSurfer.Util.Ecs;
using UnityEngine;
using UnityEngine.Assertions;
using EcsMainSystemsInitialization = CubeSurfer.EcsSystemsInitialization.MainInitialization;
using FollowingCameraEntity = CubeSurfer.EcsEntity.FollowingCamera;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;
using LevelEntity = CubeSurfer.EcsEntity.Level;

namespace CubeSurfer
{
    public partial class GameManager : MonoBehaviour
    {
        public event Action OnEntitiesCreated;
        public event Action OnEntitiesDeleting;

        public LevelEntity Level { get; private set; }
        public PlayerEntity Player { get; private set; }
        public FollowingCameraEntity FollowingCamera { get; private set; }

        public const int MaxGemsAmount = 999;

        public int GemsAmount
        {
            get => _gameSaveData.GemsAmount;
            set => _gameSaveData.GemsAmount = Mathf.Clamp(value, 0, MaxGemsAmount);
        }
        
        [Header("Prefabs")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject levelPrefab;
        [SerializeField] private GameObject followingCameraPrefab;
        
        [Header("Presets")]
        [SerializeField] private FeaturesPreset[] featuresPreset;
        [SerializeField] private ObjectsPreset[] levelObjectsPresets;
        
        
        private EcsMainSystemsInitialization _systemsInitialization;
        private Transform _thisTransform;

        private GameSaveData _gameSaveData;

        private void Awake()
        {
            Assert.IsTrue(featuresPreset.Length == levelObjectsPresets.Length);
            Assert.IsTrue(featuresPreset.Length >= 3);
            
            _thisTransform = transform;
            
            _systemsInitialization = FindObjectOfType<EcsMainSystemsInitialization>();
            
            _gameSaveData = GameSaveData.LoadFromFile();
        }

        private void OnEnable()
        {
            _systemsInitialization.OnSystemInitFinished += StartGameFirstTime;
        }

        private void OnDisable()
        {
            _systemsInitialization.OnSystemInitFinished -= StartGameFirstTime;
        }
        
        private void StartGameFirstTime()
        {
            CreateEntities();
        }
        
        public void NextLevel()
        {
            _gameSaveData.LevelIndex = NextLevelIndex(_gameSaveData.LevelIndex);
            
            _gameSaveData.Save();
            RestartLevel();
        }

        public void RestartLevel()
        {
            DeleteEntities();
            CreateEntities();
        }

        // 0-1 - "tutorial", 2-4 - repeated.
        private int NextLevelIndex(int currentIndex)
        {
            currentIndex = Mathf.Clamp(currentIndex, 0, 4);
            if (currentIndex < featuresPreset.Length - 1)
            {
                return currentIndex + 1;
            }
            
            return 2; 
        }
    }
}
