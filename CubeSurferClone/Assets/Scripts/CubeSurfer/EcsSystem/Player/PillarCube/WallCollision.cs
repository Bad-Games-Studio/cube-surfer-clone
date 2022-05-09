using Leopotam.Ecs;
using UnityEngine;
using CubeSurfer.EcsComponent;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;
using WallCollisionEvent = CubeSurfer.EcsComponent.Player.PillarBlock.WallCollisionEvent;

namespace CubeSurfer.EcsSystem.Player.PillarCube
{
    public class WallCollision : IEcsRunSystem
    {
        private EcsFilter<PillarBlockTag, TransformRef, WallCollisionEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<TransformRef>();

                HandleWallCollisionEvent(transformRef.Transform);
            }
        }

        
        private static void HandleWallCollisionEvent(Transform pillarBlock)
        {
            if (pillarBlock.parent != null)
            {
                var pillar = pillarBlock.parent.GetComponent<EcsEntity.Player.CubesPillar>();
                pillar.DecrementBlocksCounter();
            }
            pillarBlock.parent = null;
            
            Object.Destroy(pillarBlock.gameObject, 3);
        }
    }
}