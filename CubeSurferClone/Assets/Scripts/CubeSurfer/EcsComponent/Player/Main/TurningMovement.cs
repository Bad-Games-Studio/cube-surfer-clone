using System;
using CubeSurfer.PlatformMovement;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player.Main
{
    [Serializable]
    public struct TurningMovement
    {
        public static float MaxAngle => Mathf.PI / 2;
        
        
        public float speed;

        public Quaternion globalRotation;
        public Vector3 circleCenter;
        public float circleRadius;

        
        private float _currentAngle;
        public float CurrentAngle
        {
            get => _currentAngle;
            set => _currentAngle = Mathf.Clamp(value, 0.0f, MaxAngle);
        }
        public float Progress => _currentAngle / MaxAngle;

        
        private TurnDirection _turnDirection;
        public TurnDirection TurnDirection
        {
            get => _turnDirection;
            set
            {
                _turnDirection = value;
                UpdateTargetRotation();
            }
        }


        private Quaternion _startRotation;
        public Quaternion TargetRotation { get; private set; }
        public Quaternion StartRotation
        {
            get => _startRotation;
            set
            {
                _startRotation = value;
                UpdateTargetRotation();
            }
        }
        public Quaternion InterpolatedRotation => Quaternion.Lerp(StartRotation, TargetRotation, Progress);


        private float TargetAngle(float startAngle)
        {
            return _turnDirection switch
            {
                TurnDirection.Left => startAngle - 90, // unity :ohh_yeah:
                TurnDirection.Right => startAngle + 90,
                _ => startAngle
            };
        }

        private void UpdateTargetRotation()
        {
            var eulerTarget = _startRotation.eulerAngles;
            eulerTarget.y = TargetAngle(eulerTarget.y);
            TargetRotation = Quaternion.Euler(eulerTarget);
        }
    }
}