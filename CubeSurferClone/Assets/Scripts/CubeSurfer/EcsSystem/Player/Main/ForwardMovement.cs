using CubeSurfer.EcsComponent;
using Leopotam.Ecs;
using UnityEngine;
using PlayerTag = CubeSurfer.EcsComponent.Player.Main.Tag;
using ForwardMovementComponent = CubeSurfer.EcsComponent.Player.Main.ForwardMovement;

namespace CubeSurfer.EcsSystem.Player.Main
{
    public class ForwardMovement : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, TransformRef, ForwardMovementComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<TransformRef>();
                ref var movement = ref entity.Get<ForwardMovementComponent>();
                
                HandlePlayerMovement(transformRef.Transform, ref movement);
            }
        }

        private static void HandlePlayerMovement(Transform player, ref ForwardMovementComponent forwardMovement)
        {
            if (forwardMovement.isMoving)
            {
                player.position += Time.deltaTime * forwardMovement.speed * player.forward;
                return;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                forwardMovement.isMoving = true;
            }
        }
    }
}