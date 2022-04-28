using UnityEngine;

namespace CubeSurfer.Util
{
    public static class MovementMagic
    {
        public static Vector3 NewPositionFromOffset(Transform sourceObject, Vector3 offset)
        {
            var xAxisLocal = sourceObject.right;
            var yAxisLocal = sourceObject.up;
            var zAxisLocal = sourceObject.forward;
            
            var globalOffset =
                offset.x * xAxisLocal +
                offset.y * yAxisLocal +
                offset.z * zAxisLocal;
            
            return sourceObject.position + globalOffset;
        }
    }
}