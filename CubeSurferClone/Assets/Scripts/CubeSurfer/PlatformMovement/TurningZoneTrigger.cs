using System;
using CubeSurfer.Snapping;
using UnityEngine;
using UnityEngine.Assertions;

namespace CubeSurfer.PlatformMovement
{
    public class TurningZoneTrigger : MonoBehaviour
    {
        [SerializeField] private TurnDirection turn;
        
        private Transform _circleCenterObject;
        private Transform _parent;

        private const float RadiusPercentageFromPlatformSize = 1.0f;
        private const float SingleFullTurnAngle = 90;

        private void Awake()
        {
            _circleCenterObject = GetComponentInChildren<CircleCenterTag>().transform;
            _parent = GetComponentInParent<SnappableObject>().transform;

            var scale = _parent.localScale;
            Assert.IsTrue(Mathf.Approximately(scale.x, scale.z));
        }

        public EcsComponent.Player.CircularMovement GetMovementData()
        {
            return new EcsComponent.Player.CircularMovement
            {
                speed = 0,
                circleRadius = _parent.localScale.x * RadiusPercentageFromPlatformSize,
                circleCenter = _circleCenterObject.position,
                circleRotation = _circleCenterObject.rotation,
                currentAngle = 0,
                turnDirection = turn,
            };
        }
    }
}