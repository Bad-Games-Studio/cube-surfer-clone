using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystem.Player
{
    public class CircularMovement : IEcsRunSystem
    {
        private EcsFilter<
            EcsComponent.Player.Tag,
            EcsComponent.TransformRef,
            EcsComponent.Player.CircularMovement> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<EcsComponent.TransformRef>();
                ref var circularMovementRef = ref entity.Get<EcsComponent.Player.CircularMovement>();
                
                HandleCircularMovement(transformRef.Transform, ref circularMovementRef);
            }
        }

        private static void HandleCircularMovement(
            Transform player, ref EcsComponent.Player.CircularMovement circularMovement)
        {
            var angularVelocity = circularMovement.speed / circularMovement.circleRadius;
            
        }
    }
}