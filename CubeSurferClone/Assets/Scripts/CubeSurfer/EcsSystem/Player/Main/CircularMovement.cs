using CubeSurfer.PlatformMovement;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystem.Player.Main
{
    public class CircularMovement : IEcsRunSystem
    {
        private EcsFilter<
            EcsComponent.Player.Main.Tag,
            EcsComponent.TransformRef,
            EcsComponent.Player.Main.TurningMovement> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<EcsComponent.TransformRef>();
                ref var circularMovementRef = ref entity.Get<EcsComponent.Player.Main.TurningMovement>();
                
                HandleCircularMovement(transformRef.Transform, ref circularMovementRef);
                ReplaceComponentIfDone(transformRef.Transform, ref circularMovementRef);
            }
        }

        private static void HandleCircularMovement(
            Transform player, ref EcsComponent.Player.Main.TurningMovement turningMovement)
        {
            var angularVelocity = turningMovement.speed / turningMovement.circleRadius;
            var deltaAngle = Time.deltaTime * angularVelocity;
            
            turningMovement.CurrentAngle += deltaAngle;
            
            var circlePosition = GetPositionOnCircle(ref turningMovement);
            
            player.position = turningMovement.circleCenter + turningMovement.circleRadius * circlePosition;
            player.rotation = turningMovement.InterpolatedRotation;
        }

        private static Vector3 GetPositionOnCircle(ref EcsComponent.Player.Main.TurningMovement turningMovement)
        {
            return turningMovement.globalRotation * turningMovement.TurnDirection switch
            {
                TurnDirection.Left => new Vector3
                {
                    x = Mathf.Cos(turningMovement.CurrentAngle),
                    y = 0,
                    z = Mathf.Sin(turningMovement.CurrentAngle)
                },
                TurnDirection.Right => new Vector3
                {
                    x = -Mathf.Cos(turningMovement.CurrentAngle),
                    y = 0,
                    z = Mathf.Sin(turningMovement.CurrentAngle)
                },
                _ => Vector3.zero
            };
        }
        
        private static void ReplaceComponentIfDone(
            Transform player, ref EcsComponent.Player.Main.TurningMovement turningMovement)
        {
            if (turningMovement.Progress < 1.0f)
            {
                return;
            }

            var playerEntity = player.GetComponent<EcsEntity.Player.Main>();
            playerEntity.StopCircularMovement();
        }
    }
}