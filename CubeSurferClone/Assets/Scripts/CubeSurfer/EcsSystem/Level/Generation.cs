using System;
using System.Collections.Generic;
using CubeSurfer.EcsComponent;
using CubeSurfer.EcsComponent.Level;
using CubeSurfer.LevelGeneration;
using CubeSurfer.Snapping;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityRandom = UnityEngine.Random;
using LevelTag = CubeSurfer.EcsComponent.Level.Tag;

namespace CubeSurfer.EcsSystem.Level
{
    public class Generation : IEcsRunSystem
    {
        private EcsFilter<LevelTag, TransformRef, GenerationSettings> _filter;
        private Transform _currentLevel;

        private Direction _currentDirection;
        private int _currentMaxScore;
        private List<FeatureBag> _availableFeatures;

        private enum Direction
        {
            PositiveX, NegativeX,
            PositiveZ
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                
                var transformRef = entity.Get<TransformRef>();
                _currentLevel = transformRef.Transform;
                
                ref var generationSettings = ref entity.Get<GenerationSettings>();
                
                StartGeneration(ref generationSettings);
            }
        }
        
        private void StartGeneration(ref GenerationSettings settings)
        {
            CreateAvailableFeaturesList(ref settings);

            GenerateLevel(ref settings);
        }

        private void CreateAvailableFeaturesList(ref GenerationSettings settings)
        {
            if (!settings.CanGenerateLevel)
            {
                throw new ArgumentException(
                    "Impossible to generate playable level with this preset: no bonus platforms were specified");
            }
            
            _availableFeatures = new List<FeatureBag>();

            if (settings.TurnsAmount > 0)
            {
                _availableFeatures.Add(new FeatureBag(FeatureBag.ObjectType.Turn, settings.TurnsAmount));
            }
            
            if (settings.WallsAmount > 0)
            {
                _availableFeatures.Add(new FeatureBag(FeatureBag.ObjectType.Wall, settings.WallsAmount));
            }
            
            if (settings.LavaLakesAmount > 0)
            {
                _availableFeatures.Add(new FeatureBag(FeatureBag.ObjectType.LavaLake, settings.LavaLakesAmount));
            }
        }


        private static GameObject GetRandomElement(IReadOnlyList<GameObject> array)
        {
            var randomIndex = UnityRandom.Range(0, array.Count);
            return array[randomIndex];
        }
        
        private void GenerateLevel(ref GenerationSettings settings)
        {
            _currentDirection = Direction.PositiveZ;
            _currentMaxScore = 1;

            var features = GenerateFeaturesList();
            var gameObjects = FeaturesToGameObjects(features, ref settings);
            gameObjects = InjectScoreGivers(gameObjects, ref settings);
            CreatePlatformsFrom(gameObjects);
        }

        private List<FeatureBag> GenerateFeaturesList()
        {
            var features = new List<FeatureBag>();
            while (_availableFeatures.Count > 0)
            {
                var newFeature = GetRandomFeature();
                features.Add(newFeature);
            }
            
            return features;
        }

        private FeatureBag GetRandomFeature()
        {
            var index = UnityRandom.Range(0, _availableFeatures.Count);
            var feature = _availableFeatures[index];
            
            var result = feature.TakeOne();
            
            if (feature.Empty)
            {
                _availableFeatures.RemoveAt(index);
            }

            return result;
        }

        private List<GameObject> FeaturesToGameObjects(List<FeatureBag> features, ref GenerationSettings settings)
        {
            var gameObjects = new List<GameObject> { settings.objectsPreset.startPlatform };

            foreach (var feature in features)
            {
                var currentFeatureObject = GameObjectFromFeature(feature.Type, ref settings);
                if (currentFeatureObject.TryGetComponent(out DangerousPlatform scoreKiller))
                {
                    _currentMaxScore -= scoreKiller.MinScoreToLose;
                }

                if (currentFeatureObject.TryGetComponent(out PlatformWithCollectibles scoreGiver))
                {
                    _currentMaxScore += scoreGiver.MaxScore;
                }

                while (_currentMaxScore <= settings.MinPlayerScore)
                {
                    var bonusObject = GetRandomElement(settings.objectsPreset.bonusPlatforms);
                    
                    _currentMaxScore += bonusObject.GetComponent<PlatformWithCollectibles>().MaxScore;
                    gameObjects.Add(bonusObject);
                }
                gameObjects.Add(currentFeatureObject);
            }
            
            gameObjects.Add(settings.objectsPreset.finishPlatform);
            
            return gameObjects;
        }

        private GameObject GameObjectFromFeature(FeatureBag.ObjectType type, ref GenerationSettings settings)
        {
            return type switch
            {
                FeatureBag.ObjectType.Wall => GetRandomElement(settings.objectsPreset.wallsPlatforms),
                FeatureBag.ObjectType.LavaLake => GetRandomElement(settings.objectsPreset.lavaPlatforms),
                FeatureBag.ObjectType.Turn => GetRandomTurnDirection(ref settings),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private GameObject GetRandomTurnDirection(ref GenerationSettings settings)
        {
            switch (_currentDirection)
            {
                case Direction.PositiveZ:
                    var decision = UnityRandom.Range(0.0f, 0.1f);
                    _currentDirection = decision < 0.5f ? 
                        Direction.NegativeX : 
                        Direction.PositiveX;
                    return decision < 0.5f ? 
                        settings.objectsPreset.turnLeftPlatform : 
                        settings.objectsPreset.turnRightPlatform;
                
                case Direction.NegativeX:
                    _currentDirection = Direction.PositiveZ;
                    return settings.objectsPreset.turnRightPlatform;
                
                case Direction.PositiveX:
                    _currentDirection = Direction.PositiveZ;
                    return settings.objectsPreset.turnLeftPlatform;
                
                default:
                    return settings.objectsPreset.standardPlatform;
            }
        }

        private List<GameObject> InjectScoreGivers(List<GameObject> gameObjects, ref GenerationSettings settings)
        {
            while (_currentMaxScore < settings.MinPlayerScore)
            {
                var index = UnityRandom.Range(1, gameObjects.Count - 1);
                
                var bonusObject = GetRandomElement(settings.objectsPreset.bonusPlatforms);
                    
                _currentMaxScore += bonusObject.GetComponent<PlatformWithCollectibles>().MaxScore;

                gameObjects.Insert(index, bonusObject);
            }
            
            return gameObjects;
        }
        
        private void CreatePlatformsFrom(List<GameObject> gameObjects)
        {
            var previousObject = CreatePlatform(gameObjects[0], null);
            for (var i = 1; i < gameObjects.Count; ++i)
            {
                previousObject = CreatePlatform(gameObjects[i], previousObject);
            }
        }

        private GameObject CreatePlatform(GameObject newPlatform, [CanBeNull] GameObject previousPlatform)
        {
            var platform = 
                UnityObject.Instantiate(newPlatform, Vector3.zero, quaternion.identity);
            platform.transform.parent = _currentLevel;

            if (previousPlatform == null)
            {
                return platform;
            }

            var source = platform.GetComponent<SnapPointsHolder>();
            var destination = previousPlatform.GetComponent<SnapPointsHolder>();
            source.SnapTo(destination);

            return platform;
        }
    }
}