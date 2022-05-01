using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player.CubesPillar
{
    [Serializable]
    public struct HorizontalMovement
    {
        [Range(0.1f, 10.0f)]
        public float movementSensitivity;
        
        public float areaWidth;
        public float LeftEdge => -areaWidth / 2.0f;
        public float RightEdge => areaWidth / 2.0f;

        
        public float ValidateHorizontalPosition(float updatedPosition)
        {
            updatedPosition = Mathf.Min(updatedPosition, RightEdge);
            updatedPosition = Mathf.Max(updatedPosition, LeftEdge);
            return updatedPosition;
        }
    }
}