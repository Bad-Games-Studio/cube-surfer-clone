using System;
using CubeSurfer.CollisionTag;
using UnityEngine;

namespace CubeSurfer.LevelGeneration
{
    public class PlatformWithCollectibles : MonoBehaviour
    {
        [SerializeField] private int maxScore;
        public int MaxScore => maxScore;
    }
}