using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystem.Player
{
    public class Movement : IEcsRunSystem
    {
        private EcsFilter<
            EcsComponent.Player.Tag,
            EcsComponent.TransformRef,
            //EcsComponent.RigidbodyRef, 
            EcsComponent.Player.Movement> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<EcsComponent.TransformRef>();
                //ref var rigidbodyRef = ref entity.Get<EcsComponent.RigidbodyRef>();
                ref var movement = ref entity.Get<EcsComponent.Player.Movement>();
                //HandlePlayerMovement(transformRef.Transform, rigidbodyRef.Rigidbody, ref movement);
                HandlePlayerMovement(transformRef.Transform, ref movement);
            }
        }

        private static void HandlePlayerMovement(
            Transform transform, Rigidbody rigidbody, ref EcsComponent.Player.Movement movement)
        {
            var direction = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector3.left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector3.right;
            }
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector3.forward;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector3.back;
            }

            direction = Vector3.ClampMagnitude(direction, 1.0f);

            rigidbody.velocity = movement.speed * direction;
        }
        
        private static void HandlePlayerMovement(
            Transform transform, ref EcsComponent.Player.Movement movement)
        {
            var direction = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector3.left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector3.right;
            }
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector3.forward;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector3.back;
            }

            direction = Vector3.ClampMagnitude(direction, 1.0f);

            transform.position += Time.deltaTime * movement.speed * direction;
        }
    }
}