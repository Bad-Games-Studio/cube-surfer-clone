using System;

namespace CubeSurfer.EcsComponent.Player.Main
{
    [Serializable]
    public struct ForwardMovement
    {
        public float speed;

        public bool isMoving;
    }
}