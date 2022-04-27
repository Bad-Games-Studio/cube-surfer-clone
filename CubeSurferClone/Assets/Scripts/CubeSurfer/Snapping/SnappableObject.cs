using CubeSurfer.EcsEntity;
using UnityEngine;

namespace CubeSurfer.Snapping
{
    public class SnappableObject : MonoBehaviour
    {
        public SnapPoint BackSnapPoint { get; private set; }
        public SnapPoint FrontSnapPoint { get; private set; }

        private void Awake()
        {
            BackSnapPoint = GetComponentInChildren<BackSnapPoint>();
            FrontSnapPoint = GetComponentInChildren<FrontSnapPoint>();
        }

        /// <summary>
        /// Snaps current object's Back Snap Point to other object's Front Snap Point.
        /// </summary>
        /// <param name="other"></param>
        public void SnapTo(SnappableObject other)
        {
            var t = transform;
            t.rotation = other.FrontSnapPoint.Rotation;
            t.position = other.FrontSnapPoint.GlobalPosition - BackSnapPoint.Offset;
        }
    }
}
