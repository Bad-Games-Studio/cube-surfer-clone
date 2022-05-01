using CubeSurfer.EcsComponent.Player.Main;
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
        
        private void Awake()
        {
            _circleCenterObject = GetComponentInChildren<CircleCenterTag>().transform;
            _parent = GetComponentInParent<SnapPointsHolder>().transform;

            var scale = _parent.localScale;
            Assert.IsTrue(Mathf.Approximately(scale.x, scale.z));
        }

        public TurningMovement GetMovementData()
        {
            return new TurningMovement
            {
                CurrentAngle = 0,
                circleRadius = _parent.localScale.x,
                circleCenter = _circleCenterObject.position,
                globalRotation = _circleCenterObject.rotation,
                TurnDirection = turn
            };
        }
    }
}