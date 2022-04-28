using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Camera
{
    [Serializable]
    public struct Settings
    {
        public Transform target;

        public Vector3 offset;
        public Vector3 lookAtOffset;
    }
}