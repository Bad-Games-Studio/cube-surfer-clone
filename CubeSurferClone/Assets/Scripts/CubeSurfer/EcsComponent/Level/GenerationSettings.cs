using System;
using CubeSurfer.LevelGeneration;
using CubeSurfer.LevelGeneration.Presets;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Level
{
    [Serializable]
    public struct GenerationSettings
    {
        private const float MinPercentage = 0.05f;
        private const float MaxPercentage = 0.20f;

        [Range(20, 100)] public int platformsAmount;

        [Range(0, 10)] public int turns;
        [Range(0, 10)] public int lavaLakes;

        public LevelObjectsPreset preset;
    }
}