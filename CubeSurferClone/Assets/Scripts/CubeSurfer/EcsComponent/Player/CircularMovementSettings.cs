using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player
{
    [Serializable]
    public struct CircularMovementSettings
    {
        public float speed;

        public Vector3 circleCenter;
        public float circleRadius;
    }
}