using System;
using UnityEngine;

namespace CubeSurfer.EcsComponent.Player.PillarBlock
{
    [Serializable]
    public struct WallCollisionEvent
    {
        public Transform wall;
    }
}