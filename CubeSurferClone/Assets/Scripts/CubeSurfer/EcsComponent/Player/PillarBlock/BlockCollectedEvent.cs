using System;
using CubeSurfer.CollisionTag;

namespace CubeSurfer.EcsComponent.Player.PillarBlock
{
    [Serializable]
    public struct BlockCollectedEvent
    {
        public CollectiblePillarBlock block;
    }
}