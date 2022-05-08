using CubeSurfer.EcsComponent;
using Leopotam.Ecs;
using UnityEngine;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;

namespace CubeSurfer.EcsSystem.Player.PillarCube
{
    public class PositionAlignment : IEcsRunSystem
    {
        private EcsFilter<PillarBlockTag, TransformRef> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<TransformRef>();
                
                AdjustPosition(transformRef.Transform);    
            }
        }

        private static void AdjustPosition(Transform pillarBlock)
        {
            if (pillarBlock.parent == null)
            {
                return;
            }
            
            var pillarPosition = pillarBlock.parent.position;
            pillarBlock.position = new Vector3
            {
                x = pillarPosition.x,
                y = pillarBlock.position.y,
                z = pillarPosition.z
            };
        }
    }
}