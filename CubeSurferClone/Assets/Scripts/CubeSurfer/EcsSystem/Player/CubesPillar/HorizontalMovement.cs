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
            if (Input.touchCount > 0)
            {
                HandleMovementByTouch(transform, ref horizontalMovement);
                return;
            }
            
            HandleMovementByKeyboard(transform, ref horizontalMovement);
        }

        private static void HandleMovementByTouch(Transform transform, ref MovementComponent horizontalMovement)
        {
            var delta = GetTouchDelta();
            horizontalMovement.CurrentScreenPosition += delta * horizontalMovement.movementSensitivity;
            
            var localPosition = transform.localPosition;
            localPosition.x = horizontalMovement.MappedPosition;

            transform.localPosition = localPosition;
        }

        private static void HandleMovementByKeyboard(Transform transform, ref MovementComponent horizontalMovement)
        {
            var direction = GetDirectionFromKeyboard();
            
            var localPosition = transform.localPosition;
            localPosition.x += direction * horizontalMovement.keyboardSensitivity * Time.deltaTime;
            localPosition.x = horizontalMovement.Clamp(localPosition.x);
            
            transform.localPosition = localPosition;
        }
        
        private static float GetTouchDelta()
        {
            var deltaX = Input.touches[0].deltaPosition.x;
            if (Mathf.Abs(deltaX) > 1)
            {
                return deltaX;
            }

            return 0;
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
    }
}