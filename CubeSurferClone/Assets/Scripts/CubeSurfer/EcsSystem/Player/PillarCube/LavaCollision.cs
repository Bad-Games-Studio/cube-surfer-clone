using CubeSurfer.EcsComponent;
using Leopotam.Ecs;
using UnityEngine;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;
using TouchedLavaEvent = CubeSurfer.EcsComponent.Player.PillarBlock.TouchedLavaEvent;

namespace CubeSurfer.EcsSystem.Player.PillarCube
{
    public class LavaCollision : IEcsRunSystem
    {
        private EcsFilter<PillarBlockTag, TransformRef, TouchedLavaEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<TransformRef>();
                
                HandleTouchedLavaEvent(transformRef.Transform);
            }
        }

        private static void HandleTouchedLavaEvent(Transform cube)
        {
            var pillar = cube.parent.GetComponent<EcsEntity.Player.CubesPillar>();
            pillar.DecrementBlocksCounter();
            
            Object.Destroy(cube.gameObject);
        }
    }
}