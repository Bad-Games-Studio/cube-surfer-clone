using System;
using CubeSurfer.PlatformMovement;
using Unity.Mathematics;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player
{
    [Serializable]
    public struct CircularMovement
    {
        public float speed;

        public Quaternion circleRotation;
        public Vector3 circleCenter;
        public float circleRadius;
        
        public float currentAngle;
        public float MaxAngle => Mathf.PI / 2;
        public float Progress => currentAngle / MaxAngle;
        
        public TurnDirection turnDirection;
        
        
        private Quaternion _startRotation;
        public Quaternion StartRotation
        {
            get => _startRotation;
            set
            {
                _startRotation = value;
                var eulerTarget = value.eulerAngles;
                eulerTarget.y = TargetAngle(eulerTarget.y);
                TargetRotation = Quaternion.Euler(eulerTarget);
            }
        }
        public Quaternion TargetRotation { get; private set; }
        public Quaternion InterpolatedRotation => Quaternion.Lerp(StartRotation, TargetRotation, Progress);


        public const float FullTurnAngle = 90;
        private float TargetAngle(float startAngle) => turnDirection switch
        {
            TurnDirection.Left => startAngle - FullTurnAngle,
            TurnDirection.Right => startAngle + FullTurnAngle,
            _ => startAngle
        };
    }
}