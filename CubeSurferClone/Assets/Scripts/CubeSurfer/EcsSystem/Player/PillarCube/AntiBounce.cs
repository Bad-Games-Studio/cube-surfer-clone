using CubeSurfer.EcsComponent;
using Leopotam.Ecs;
using UnityEngine;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;

namespace CubeSurfer.EcsSystem.Player.PillarCube
{
    public class AntiBounce : IEcsRunSystem
    {
        private EcsFilter<PillarBlockTag, RigidbodyRef> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var rigidbodyRef = ref entity.Get<RigidbodyRef>();
                
                CancelBouncing(rigidbodyRef.Rigidbody);
            }
        }

        private static void CancelBouncing(Rigidbody cubeRigidbody)
        {
            if (cubeRigidbody.velocity.y < 0)
            {
                return;
            }

            cubeRigidbody.velocity = Vector3.zero;
        }
    }
}