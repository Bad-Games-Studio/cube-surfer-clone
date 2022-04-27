using UnityEngine;

namespace CubeSurfer.Snapping
{
    public class SnapPoint : MonoBehaviour
    {
        public Vector3 GlobalPosition => transform.position;
        
        public Vector3 Offset => -transform.parent.position + transform.position;

        public Quaternion Rotation => transform.rotation;
    }
}
