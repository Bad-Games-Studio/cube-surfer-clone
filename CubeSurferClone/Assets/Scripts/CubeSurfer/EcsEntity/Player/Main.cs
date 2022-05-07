using CubeSurfer.CollisionTag;
using CubeSurfer.PlatformMovement;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using ForwardMovement = CubeSurfer.EcsComponent.Player.Main.ForwardMovement;
using TurningMovement = CubeSurfer.EcsComponent.Player.Main.TurningMovement;

namespace CubeSurfer.EcsEntity.Player
{
    public class Main : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private ForwardMovement forwardMovement;
        
        private Leopotam.Ecs.EcsEntity _entity;

        private bool _levelFinished;
        private int _scoreMultiplier;

        public void CreateEntityIn(EcsWorld world)
        {
            _levelFinished = false;
            
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.Main.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            StartMovingForward();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TurningZoneTrigger turningZone))
            {
                StartCircularMovement(turningZone.GetMovementData());
            }

            if (other.TryGetComponent(out FinishLine _))
            {
                MarkLevelFinish();
            }
        }

        private void StartMovingForward()
        {
            ref var forwardMovementRef = ref _entity.Get<ForwardMovement>();
            forwardMovementRef = forwardMovement;
        }

        public void StopMovingForward()
        {
            forwardMovement = _entity.Get<ForwardMovement>();
            _entity.Del<ForwardMovement>();
        }

        public void StartCircularMovement(TurningMovement turningMovement)
        {
            turningMovement.speed = forwardMovement.speed;
            turningMovement.StartRotation = transform.rotation;

            StopMovingForward();

            ref var circularMovementRef = ref _entity.Get<TurningMovement>();
            circularMovementRef = turningMovement;
        }

        public void StopCircularMovement()
        {
            _entity.Del<TurningMovement>();
            
            StartMovingForward();
        }
        
        private void MarkLevelFinish()
        {
            _levelFinished = true;
        }

        public void SetScoreMultiplier(int newMultiplier)
        {
            _scoreMultiplier = newMultiplier;
        }
        
        public void MarkDead()
        {
            StopMovingForward();

            Debug.Log(_levelFinished ? $"Nice! Won a {_scoreMultiplier} multiplier" : "status: ded");
        }
    }
}
