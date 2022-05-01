using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystem.Player.Main
{
    public class ForwardMovement : IEcsRunSystem
    {
        private EcsFilter<
            EcsComponent.Player.Main.Tag,
            EcsComponent.TransformRef,
            EcsComponent.Player.Main.ForwardMovement> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<EcsComponent.TransformRef>();
                ref var movement = ref entity.Get<EcsComponent.Player.Main.ForwardMovement>();
                
                HandlePlayerMovement(transformRef.Transform, ref movement);
            }
        }

        private static void HandlePlayerMovement(
            Transform player, ref EcsComponent.Player.Main.ForwardMovement forwardMovement)
        {
            player.position += Time.deltaTime * forwardMovement.speed * player.forward;
        }
    }
}