using System;
using CubeSurfer.EcsComponent.Level;
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

        private int _turnsAmount;
        private int _lavaLakesAmount;
        private int _regularPlatformsAmount;

        public void Init()
        {
            ref var settings = ref _level.GenerationSettings;
            
            CalculatePlatformAmounts(ref settings);

            GenerateLevel(ref settings);
        }

        private void CalculatePlatformAmounts(ref GenerationSettings settings)
        {
            _turnsAmount = 0;
            _lavaLakesAmount = 0;
            
            if (settings.platformsAmount < 10)
            {
                return;
            }
            
            if (settings.turns)
            {
                _turnsAmount = (int) (settings.turnsPercentage * settings.platformsAmount);
            }

            if (settings.lavaLakes)
            {
                _lavaLakesAmount = (int) (settings.lavaPercentage * settings.platformsAmount);
            }

            _regularPlatformsAmount = settings.platformsAmount - _turnsAmount - _lavaLakesAmount;
        }

        private void GenerateLevel(ref GenerationSettings settings)
        {
            var previousObject = CreatePlatform(settings.startPlatform, null);
            for (var i = 0; i < _regularPlatformsAmount; ++i)
            {
                var p = i % 3;
                previousObject = CreatePlatform(settings.regularPlatforms[p], previousObject);
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

            var source = platform.GetComponent<SnappableObject>();
            var destination = previousPlatform.GetComponent<SnappableObject>();
            source.SnapTo(destination);

            return platform;
        }
    }
}