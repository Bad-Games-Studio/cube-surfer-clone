using CubeSurfer.EcsComponent.Player.CubesPillar;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystem.Player.CubesPillar
{
    public class HorizontalMovement : IEcsRunSystem
    {
        private EcsFilter<
            Tag,
            EcsComponent.TransformRef,
            EcsComponent.Player.CubesPillar.HorizontalMovement> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                var transformRef = entity.Get<EcsComponent.TransformRef>();
                ref var cubesPillarMovement = ref entity.Get<EcsComponent.Player.CubesPillar.HorizontalMovement>();
                HandlePillarMovement(transformRef.Transform, ref cubesPillarMovement);
            }
        }

        private static void HandlePillarMovement(
            Transform transform, ref EcsComponent.Player.CubesPillar.HorizontalMovement horizontalMovement)
        {
            var direction = 0;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction -= 1;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += 1;
            }
            
            var localPosition = transform.localPosition;
            localPosition.x += direction * horizontalMovement.movementSensitivity * Time.deltaTime;
            localPosition.x = horizontalMovement.ValidateHorizontalPosition(localPosition.x);
            
            transform.localPosition = localPosition;
        }
    }
}