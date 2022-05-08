using System;
using CubeSurfer.Util.Ecs;
using UnityEngine;
using EcsMainSystemsInitialization = CubeSurfer.EcsSystemsInitialization.MainInitialization;
using FollowingCameraEntity = CubeSurfer.EcsEntity.FollowingCamera;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;
using LevelEntity = CubeSurfer.EcsEntity.Level;

namespace CubeSurfer
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnEntitiesCreated;
        public event Action OnEntitiesDeleting;
        
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject levelPrefab;
        [SerializeField] private GameObject followingCameraPrefab;

        public LevelEntity Level { get; private set; }
        public PlayerEntity Player { get; private set; }
        public FollowingCameraEntity FollowingCamera { get; private set; }
        
        private EcsMainSystemsInitialization _systemsInitialization;
        private Transform _thisTransform;

        private void Awake()
        {
            _thisTransform = transform;
            
            _systemsInitialization = FindObjectOfType<EcsMainSystemsInitialization>();
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
            InstantiatePrefabs();
            OnEntitiesCreated?.Invoke();
        }

        private void InstantiatePrefabs()
        {
            CreateLevel();
            CreatePlayer();
            CreateFollowingCamera();

            for (var i = 0; i < _thisTransform.childCount; ++i)
            {
                var child = _thisTransform.GetChild(i);
                var ecsEntity = child.GetComponent<IEcsWorldEntity>();
                ecsEntity.CreateEntityIn(_systemsInitialization.World);
            }
        }

        private void CreateLevel()
        {
            var level = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            var levelTransform = level.transform;
            levelTransform.parent = _thisTransform;
            
            Level = levelTransform.GetComponent<LevelEntity>();
        }

        private void CreatePlayer()
        {
            var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            var playerTransform = player.transform;
            playerTransform.parent = _thisTransform;

            var levelTransform = Level.transform;
            var levelOffset = levelTransform.localScale.y / 2;
            var playerOffset = playerTransform.localScale.y / 2;
            var playerPosition = levelTransform.position;
            playerPosition.y += levelOffset + playerOffset;
            playerTransform.position = playerPosition;

            Player = playerTransform.GetComponent<PlayerEntity>();
        }

        private void CreateFollowingCamera()
        {
            var followingCamera = Instantiate(followingCameraPrefab, Vector3.zero, Quaternion.identity);
            var followingCameraTransform = followingCamera.transform;
            followingCameraTransform.parent = _thisTransform;
            
            FollowingCamera = followingCameraTransform.GetComponent<FollowingCameraEntity>();
            FollowingCamera.SetTarget(Player.transform);
        }
    }
}
