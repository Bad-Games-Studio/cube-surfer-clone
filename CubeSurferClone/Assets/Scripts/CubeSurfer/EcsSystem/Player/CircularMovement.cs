using System;
using CubeSurfer.PlatformMovement;
using Leopotam.Ecs;
using Unity.Mathematics;
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
                ReplaceComponentIfDone(transformRef.Transform, ref circularMovementRef);
            }
        }

        private static void HandleCircularMovement(
            Transform player, ref EcsComponent.Player.CircularMovement circularMovement)
        {
            var angularVelocity = circularMovement.speed / circularMovement.circleRadius;
            var deltaAngle = Time.deltaTime * angularVelocity;
            
            circularMovement.currentAngle += deltaAngle;
            if (circularMovement.currentAngle > circularMovement.MaxAngle)
            {
                circularMovement.currentAngle = circularMovement.MaxAngle;
            }
            
            var offset = GetPositionOnCircle(ref circularMovement);
            
            player.position = circularMovement.circleCenter + circularMovement.circleRadius * offset;
            player.rotation = circularMovement.InterpolatedRotation;
        }

        private static Vector3 GetPositionOnCircle(ref EcsComponent.Player.CircularMovement circularMovement)
        {
            return circularMovement.circleRotation * circularMovement.turnDirection switch
            {
                TurnDirection.Left => new Vector3
                {
                    x = Mathf.Cos(circularMovement.currentAngle),
                    y = 0,
                    z = Mathf.Sin(circularMovement.currentAngle),
                },
                TurnDirection.Right => new Vector3
                {
                    x = -Mathf.Cos(circularMovement.currentAngle),
                    y = 0,
                    z = Mathf.Sin(circularMovement.currentAngle),
                },
                _ => Vector3.zero
            };
        }
        
        private static void ReplaceComponentIfDone(
            Transform player, ref EcsComponent.Player.CircularMovement circularMovement)
        {
            if (circularMovement.Progress < 1.0f)
            {
                return;
            }

            var playerEntity = player.GetComponent<EcsEntity.Player>();
            playerEntity.StopCircularMovement();
        }
    }
}