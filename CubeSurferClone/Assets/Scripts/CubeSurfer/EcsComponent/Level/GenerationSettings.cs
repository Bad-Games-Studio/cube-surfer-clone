using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Level
{
    [Serializable]
    public struct GenerationSettings
    {
        [Header("Features")]
        public bool walls;
        public bool turns;
        public bool lavaLakes;

        [Range(5, 25)]
        public int platformsAmount;

        [Header("Sizes")]
        public float platformWidth;
        public float platformLength;
        public float platformHeight;
    }
}