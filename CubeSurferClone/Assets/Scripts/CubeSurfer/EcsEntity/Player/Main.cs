using CubeSurfer.PlatformMovement;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity.Player
{
    public class Main : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Player.Main.ForwardMovement forwardMovement;
        
        private Leopotam.Ecs.EcsEntity _entity;

        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.Main.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var forwardMovementRef = ref _entity.Get<EcsComponent.Player.Main.ForwardMovement>();
            forwardMovementRef = forwardMovement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TurningZoneTrigger turningZone))
            {
                StartCircularMovement(turningZone.GetMovementData());
            }
        }

        public void StartCircularMovement(EcsComponent.Player.Main.TurningMovement turningMovement)
        {
            turningMovement.speed = forwardMovement.speed;
            turningMovement.StartRotation = transform.rotation;

            _entity.Del<EcsComponent.Player.Main.ForwardMovement>();

            ref var circularMovementRef = ref _entity.Get<EcsComponent.Player.Main.TurningMovement>();
            circularMovementRef = turningMovement;
        }

        public void StopCircularMovement()
        {
            _entity.Del<EcsComponent.Player.Main.TurningMovement>();
            
            ref var forwardMovementRef = ref _entity.Get<EcsComponent.Player.Main.ForwardMovement>();
            forwardMovementRef = forwardMovement;
        }
    }
}
