using UnityEngine;
using FollowingCameraEntity = CubeSurfer.EcsEntity.FollowingCamera;
using PlayerEntity = CubeSurfer.EcsEntity.Player.Main;
using LevelEntity = CubeSurfer.EcsEntity.Level;

namespace CubeSurfer
{
    public partial class GameManager
    {
        private void CreateEntities()
        {
            InstantiatePrefabs();
            OnEntitiesCreated?.Invoke();
        }

        private void DeleteEntities()
        {
            OnEntitiesDeleting?.Invoke();
            DestroyPrefabs();
        }

        private void InstantiatePrefabs()
        {
            CreateLevel();
            CreatePlayer();
            CreateFollowingCamera();
        }
        
        private void DestroyPrefabs()
        {
            Destroy(FollowingCamera.gameObject);
            Destroy(Player.gameObject);
            Destroy(Level.gameObject);
        }

        private void CreateLevel()
        {
            var level = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            var levelTransform = level.transform;
            levelTransform.parent = _thisTransform;
            
            Level = levelTransform.GetComponent<LevelEntity>();
            Level.GenerationSettings.featuresPreset = featuresPreset[_gameSaveData.LevelIndex];
            Level.GenerationSettings.objectsPreset = levelObjectsPresets[_gameSaveData.LevelIndex];
            
            Level.CreateEntityIn(_systemsInitialization.World);
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
            Player.CreateEntityIn(_systemsInitialization.World);
        }

        private void CreateFollowingCamera()
        {
            var followingCamera = Instantiate(followingCameraPrefab, Vector3.zero, Quaternion.identity);
            var followingCameraTransform = followingCamera.transform;
            followingCameraTransform.parent = _thisTransform;
            
            FollowingCamera = followingCameraTransform.GetComponent<FollowingCameraEntity>();
            FollowingCamera.SetTarget(Player.transform);
            
            FollowingCamera.CreateEntityIn(_systemsInitialization.World);
        }
    }
}