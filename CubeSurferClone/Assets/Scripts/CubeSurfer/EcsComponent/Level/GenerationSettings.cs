using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Level
{
    [Serializable]
    public struct GenerationSettings
    {
        private const float MinPercentage = 0.05f;
        private const float MaxPercentage = 0.20f;
        
        [Header("Features")]
        [Range(5, 50)]
        public int platformsAmount;
        
        public bool walls;
        
        public bool turns;
        [Range(MinPercentage, MaxPercentage)] public float turnsPercentage;
        
        public bool lavaLakes;
        [Range(MinPercentage, MaxPercentage)] public float lavaPercentage;

        [Header("Level parts")]
        public GameObject startPlatform;
        public GameObject[] regularPlatforms;
    }
}