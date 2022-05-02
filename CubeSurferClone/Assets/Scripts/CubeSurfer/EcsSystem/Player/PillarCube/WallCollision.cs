using Leopotam.Ecs;
using UnityEngine;
using CubeSurfer.EcsComponent;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;
using WallCollisionEvent = CubeSurfer.EcsComponent.Player.PillarBlock.WallCollisionEvent;

namespace CubeSurfer.EcsSystem.Player.PillarCube
{
    public class WallCollision : IEcsRunSystem
    {
        private EcsFilter<PillarBlockTag, TransformRef, WallCollisionEvent> _wallCollisionFilter;
        
        public void Run()
        {
            foreach (var i in _wallCollisionFilter)
            {
                var entity = _wallCollisionFilter.GetEntity(i);
                var transformRef = entity.Get<TransformRef>();
                var @event = entity.Get<WallCollisionEvent>();
                
                HandleWallCollisionEvent(transformRef.Transform, ref @event);
            }
        }

        
        private static void HandleWallCollisionEvent(Transform pillarBlock, ref WallCollisionEvent @event)
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