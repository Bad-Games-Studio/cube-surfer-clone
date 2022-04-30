using CubeSurfer.PlatformMovement;
using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class Player : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Player.ForwardMovement forwardMovement;
        
        private Leopotam.Ecs.EcsEntity _entity;

        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var forwardMovementRef = ref _entity.Get<EcsComponent.Player.ForwardMovement>();
            forwardMovementRef = forwardMovement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TurningZoneTrigger turningZone))
            {
                StartCircularMovement(turningZone.GetMovementData());
            }
        }

        public void StartCircularMovement(EcsComponent.Player.CircularMovement circularMovement)
        {
            circularMovement.speed = forwardMovement.speed;
            circularMovement.StartRotation = transform.rotation;

            _entity.Del<EcsComponent.Player.ForwardMovement>();

            ref var circularMovementRef = ref _entity.Get<EcsComponent.Player.CircularMovement>();
            circularMovementRef = circularMovement;
        }

        public void StopCircularMovement()
        {
            _entity.Del<EcsComponent.Player.CircularMovement>();
            
            ref var forwardMovementRef = ref _entity.Get<EcsComponent.Player.ForwardMovement>();
            forwardMovementRef = forwardMovement;
        }
    }
}
