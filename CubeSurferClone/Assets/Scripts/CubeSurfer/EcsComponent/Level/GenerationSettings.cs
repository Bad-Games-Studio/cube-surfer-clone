using System;
using CubeSurfer.LevelGeneration;
using CubeSurfer.LevelGeneration.Presets;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Level
{
    [Serializable]
    public struct GenerationSettings
    {
        [Range(1, 5)] public int minPlayerScore;
        
        [Range(0, 20)] public int emptyBlocks;
        [Range(0, 20)] public int walls;
        [Range(0, 10)] public int turns;
        [Range(0, 10)] public int lavaLakes;

        public LevelObjectsPreset preset;
    }
}