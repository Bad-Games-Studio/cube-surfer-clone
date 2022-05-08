using CubeSurfer.EcsComponent;
using Leopotam.Ecs;
using UnityEngine;
using PillarTag = CubeSurfer.EcsComponent.Player.CubesPillar.Tag;
using MovementComponent = CubeSurfer.EcsComponent.Player.CubesPillar.HorizontalMovement;

namespace CubeSurfer.EcsSystem.Player.CubesPillar
{
    public class HorizontalMovement : IEcsRunSystem
    {
        private EcsFilter<PillarTag, TransformRef, MovementComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<TransformRef>();
                ref var cubesPillarMovement = ref entity.Get<MovementComponent>();
                HandlePillarMovement(transformRef.Transform, ref cubesPillarMovement);
            }
        }

        private static void HandlePillarMovement(Transform transform, ref MovementComponent horizontalMovement)
        {
            var direction = Input.touchCount > 0 ? GetDirectionFromTouch() : GetDirectionFromKeyboard();
            
            var localPosition = transform.localPosition;
            localPosition.x += direction * horizontalMovement.movementSensitivity * Time.deltaTime;
            localPosition.x = horizontalMovement.ValidateHorizontalPosition(localPosition.x);
            
            transform.localPosition = localPosition;
        }

        private static int GetDirectionFromKeyboard()
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

            return direction;
        }

        private static int GetDirectionFromTouch()
        {
            var deltaX = Input.touches[0].deltaPosition.x;
            if (Mathf.Abs(deltaX) > 5)
            {
                return (int) Mathf.Sign(deltaX);
            }

            return 0;
        }
    }
}