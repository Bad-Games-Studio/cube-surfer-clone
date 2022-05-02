using Leopotam.Ecs;
using CubeSurfer.EcsComponent;
using UnityEngine;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;
using BlockCollectedEvent = CubeSurfer.EcsComponent.Player.PillarBlock.BlockCollectedEvent;

namespace CubeSurfer.EcsSystem.Player.PillarCube
{
    public class BlockCollecting : IEcsRunSystem
    {
        private EcsFilter<PillarBlockTag, TransformRef, BlockCollectedEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                var transformRef = entity.Get<TransformRef>();
                var @event = entity.Get<BlockCollectedEvent>();
                HandleBlockCollectionEvent(transformRef.Transform, ref @event);
            }
        }
        
        
        private static void HandleBlockCollectionEvent(Transform pillarBlock, ref BlockCollectedEvent @event)
        {
            if (@event.block.wasTouched)
            {
                return;
            }
            
            @event.block.wasTouched = true;
            Object.Destroy(@event.block.gameObject);
            
            var pillar = pillarBlock.parent.GetComponent<EcsEntity.Player.CubesPillar>();
            pillar.AddPillarBlock();
        }
    }
}