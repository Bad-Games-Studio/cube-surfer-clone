using CubeSurfer.Snapping;
using UnityEngine;
using UnityEngine.Assertions;

namespace CubeSurfer.CircularMovement
{
    public class ZoneTrigger : MonoBehaviour
    {
        private Transform _circleCenterObject;
        private Transform _parent;

        private const float RadiusPercentageFromPlatformSize = 0.5f;

        private void Awake()
        {
            _circleCenterObject = GetComponentInChildren<CircleCenterTag>().transform;
            _parent = transform.parent;

            var scale = _parent.localScale;
            Assert.IsTrue(Mathf.Approximately(scale.x, scale.z));
        }

        public EcsComponent.Player.CircularMovementSettings GetMovementData()
        {
            return new EcsComponent.Player.CircularMovementSettings
            {
                speed = 0,
                circleRadius = _parent.localScale.x * RadiusPercentageFromPlatformSize,
                circleCenter = _circleCenterObject.position
            };
        }
    }
}