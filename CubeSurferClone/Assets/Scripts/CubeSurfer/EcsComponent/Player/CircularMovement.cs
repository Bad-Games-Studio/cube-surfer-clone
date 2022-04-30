using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player
{
    [Serializable]
    public struct CircularMovement
    {
        public float speed;

        public Vector3 circleCenter;
        public float circleRadius;

        public float startAngle;
        public float currentAngle;
    }
}