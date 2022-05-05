using System;
using UnityEngine;

namespace CubeSurfer.Snapping
{
    public class SnapPointsHolder : MonoBehaviour
    {
        private SnapPoint BackSnapPoint { get; set; }
        private SnapPoint FrontSnapPoint { get; set; }

        private void Awake()
        {
            BackSnapPoint = GetComponentInChildren<BackSnapPoint>();
            FrontSnapPoint = GetComponentInChildren<FrontSnapPoint>();
            ValidateSnapPoints();
        }

        private void ValidateSnapPoints()
        {
            if (BackSnapPoint == null)
            {
                throw new ArgumentException("Back Snap Point was null.");
            }

            if (FrontSnapPoint == null)
            {
                throw new ArgumentException("Front Snap Point was null.");
            }
        }

        /// <summary>
        /// Snaps current object's Back Snap Point to other object's Front Snap Point.
        /// </summary>
        /// <param name="other"></param>
        public void SnapTo(SnapPointsHolder other)
        {
            var t = transform;
            t.rotation = other.FrontSnapPoint.Rotation;
            t.position = other.FrontSnapPoint.GlobalPosition - BackSnapPoint.Offset;
        }
    }
}
