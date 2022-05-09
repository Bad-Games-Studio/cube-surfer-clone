using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player.CubesPillar
{
    [Serializable]
    public struct HorizontalMovement
    {
        [Range(0.1f, 2.0f)] public float movementSensitivity;
        [Range(0.1f, 10.0f)] public float keyboardSensitivity;
        
        public float areaWidth;
        public float LeftEdge => -areaWidth / 2.0f;
        public float RightEdge => areaWidth / 2.0f;


        [NonSerialized] private float _currentScreenPosition;

        public float CurrentScreenPosition
        {
            get => _currentScreenPosition;
            set => _currentScreenPosition = Mathf.Clamp(value, 0, Screen.width);
        }
        
        public float MappedPosition => CurrentScreenPosition / Screen.width * areaWidth + LeftEdge;
        public static float InitialScreenPosition => Screen.width / 2.0f;


        public float Clamp(float position) => Mathf.Clamp(position, LeftEdge, RightEdge);
        public float ValidateHorizontalPosition(float updatedPosition)
        {
            updatedPosition = Mathf.Min(updatedPosition, RightEdge);
            updatedPosition = Mathf.Max(updatedPosition, LeftEdge);
            return updatedPosition;
        }
    }
}