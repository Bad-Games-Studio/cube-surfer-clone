using System;
using CubeSurfer.LevelGeneration;
using CubeSurfer.LevelGeneration.Presets;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Level
{
    [Serializable]
    public struct GenerationSettings
    {
        public FeaturesPreset featuresPreset;

        public ObjectsPreset objectsPreset;


        public int MinPlayerScore => featuresPreset.minPlayerScore;
        
        public int TurnsAmount => featuresPreset.turns;
        public int WallsAmount => objectsPreset.wallsPlatforms.Length == 0 ? 0 : featuresPreset.walls;
        public int LavaLakesAmount => objectsPreset.lavaPlatforms.Length == 0 ? 0 : featuresPreset.lavaLakes;
        public int BonusPlatformsAmount => objectsPreset.bonusPlatforms.Length;

        public bool HasObstacles => WallsAmount > 0 || LavaLakesAmount > 0;
        public bool HasBonusPlatforms => BonusPlatformsAmount > 0;

        public bool CanGenerateLevel => !HasObstacles || HasBonusPlatforms;
    }
}