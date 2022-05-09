using System;
using UnityEngine;

namespace CubeSurfer.Snapping
{
    public class SnapPoint : MonoBehaviour
    {
        private Transform _myTransform;
        private void Awake()
        {
            _myTransform = transform;
        }

        public Vector3 GlobalPosition => _myTransform.position;
        
        public Vector3 Offset(Vector3 parentPosition) => -parentPosition + _myTransform.position;

        public Quaternion Rotation => _myTransform.rotation;
    }
}
