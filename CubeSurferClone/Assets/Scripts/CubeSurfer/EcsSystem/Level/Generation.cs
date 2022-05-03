using System;
using System.Collections.Generic;
using CubeSurfer.EcsComponent.Level;
using CubeSurfer.LevelGeneration;
using CubeSurfer.Snapping;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace CubeSurfer.EcsSystem.Level
{
    public class Generation : IEcsInitSystem
    {
        private EcsEntity.Level _level;

        private int _turnsEveryN;
        private int _turnsLeft;
        private int _lavaLakesLeft;
        private int _wallsLeft;
        private int FeaturesLeft => _turnsLeft + _lavaLakesLeft + _wallsLeft;

        private int _currentPlatformIndex;
        private Direction _currentDirection;
        private int _currentMaxScore;
        private List<Feature> _availableFeatures;


        public void Init()
        {
            ref var settings = ref _level.GenerationSettings;
            
            CalculatePlatformAmounts(ref settings);

            GenerateLevel(ref settings);
        }

        private void CalculatePlatformAmounts(ref GenerationSettings settings)
        {
            _turnsLeft = settings.turns;
            _lavaLakesLeft = settings.lavaLakes;

            _wallsLeft = (settings.platformsAmount - _turnsLeft - _lavaLakesLeft) / 2;

            _turnsEveryN = settings.platformsAmount / (settings.turns + 1);
        }

        private enum Feature
        {
            Wall, Turn, LavaLake
        }

        private enum Direction
        {
            PositiveX, NegativeX,
            PositiveZ
        }
        
        private void GenerateLevel(ref GenerationSettings settings)
        {
            var previousObject = CreatePlatform(settings.preset.startPlatform, null);

            _currentPlatformIndex = 0;
            _currentMaxScore = 0;
            _availableFeatures = new List<Feature>{ Feature.Wall, Feature.Turn, Feature.LavaLake };
            _currentDirection = Direction.PositiveZ;

            while (FeaturesLeft > 0 && _currentPlatformIndex < settings.platformsAmount)
            {
                var feature = DecideNextFeature();
                previousObject = GenerateFeature(feature, ref settings, previousObject);
                ++_currentPlatformIndex;
            }
        }

        private Feature DecideNextFeature()
        {
            if (_wallsLeft == 0)
            {
                _availableFeatures.Remove(Feature.Wall);
            }
            
            if (_lavaLakesLeft == 0)
            {
                _availableFeatures.Remove(Feature.LavaLake);
            }

            if (_turnsLeft == 0)
            {
                _availableFeatures.Remove(Feature.Turn);
            }
            else if (_currentPlatformIndex % _turnsEveryN == _turnsEveryN - 1)
            {
                return Feature.Turn;
            }
            
            var feature = _availableFeatures[Random.Range(0, _availableFeatures.Count)];
            return feature;
        }

        private GameObject GetFeatureObject(Feature feature, ref GenerationSettings settings)
        {
            return feature switch
            {
                Feature.Wall => GetRandomElement(settings.preset.wallsPlatforms),
                Feature.Turn => GetTurnObject(ref settings),
                Feature.LavaLake => GetRandomElement(settings.preset.lavaPlatforms),
                _ => throw new ArgumentOutOfRangeException(nameof(feature), feature, null)
            };
        }

        private static GameObject GetRandomElement(IReadOnlyList<GameObject> array)
        {
            var randomIndex = Random.Range(0, array.Count);
            return array[randomIndex];
        }

        private GameObject GetTurnObject(ref GenerationSettings settings)
        {
            var left = settings.preset.turnLeftPlatform;
            var right = settings.preset.turnRightPlatform;
            switch (_currentDirection)
            {
                case Direction.PositiveZ:
                    var decision = Random.Range(0.0f, 1.0f);
                    _currentDirection = decision < 0.5f ? Direction.NegativeX : Direction.PositiveX;
                    return decision < 0.5 ? left : right;
                
                case Direction.PositiveX:
                    _currentDirection = Direction.PositiveZ;
                    return left;
                
                case Direction.NegativeX:
                    _currentDirection = Direction.PositiveZ;
                    return right;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GameObject GenerateFeature(Feature feature, ref GenerationSettings settings, GameObject previousObject)
        {
            var featureObject = GetFeatureObject(feature, ref settings);
            
            DecrementFeatureCounter(feature);
            
            if (feature == Feature.Turn)
            {
                return CreatePlatform(featureObject, previousObject);
            }

            var minScoreToLose = featureObject.GetComponent<DangerousPlatform>().MinScoreToLose;
            _currentMaxScore -= minScoreToLose;
            while (_currentMaxScore <= 1)
            {
                var bonusPlatform = GetRandomElement(settings.preset.bonusPlatforms);
                
                var platformMaxScore = bonusPlatform.GetComponent<PlatformWithCollectibles>().MaxScore;
                _currentMaxScore += platformMaxScore;

                previousObject = CreatePlatform(bonusPlatform, previousObject);
                ++_currentPlatformIndex;
            }
            
            return CreatePlatform(featureObject, previousObject);
        }

        private void DecrementFeatureCounter(Feature feature)
        {
            switch (feature)
            {
                case Feature.Wall:
                    --_wallsLeft;
                    break;
                case Feature.Turn:
                    --_turnsLeft;
                    break;
                case Feature.LavaLake:
                    --_lavaLakesLeft;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(feature), feature, null);
            }
        }

        private GameObject CreatePlatform(GameObject newPlatform, [CanBeNull] GameObject previousPlatform)
        {
            var platform = 
                Object.Instantiate(newPlatform, Vector3.zero, quaternion.identity);
            platform.transform.parent = _level.transform;

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