using System;
using CubeSurfer.CollisionTag;
using CubeSurfer.PlatformMovement;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using ForwardMovement = CubeSurfer.EcsComponent.Player.Main.ForwardMovement;
using TurningMovement = CubeSurfer.EcsComponent.Player.Main.TurningMovement;
using PillarHorizontalMovement = CubeSurfer.EcsComponent.Player.CubesPillar.HorizontalMovement;

namespace CubeSurfer.EcsEntity.Player
{
    public class Main : MonoBehaviour, IEcsWorldEntity
    {
        public event Action OnLevelCompleted;
        public event Action OnDied;
        
        
        [SerializeField] private GameObject pillarBlockPrefab;
        [SerializeField] private ForwardMovement forwardMovement;
        [SerializeField] private PillarHorizontalMovement horizontalMovement;
        
        
        private Leopotam.Ecs.EcsEntity _entity;

        private bool _finishReached;
        public int ScoreMultiplier { get; private set; }

        
        public void CreateEntityIn(EcsWorld world)
        {
            _finishReached = false;
            
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.Main.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            InitializePillarIn(world);
            
            StartMovingForward();
        }

        private void InitializePillarIn(EcsWorld world)
        {
            horizontalMovement.CurrentScreenPosition = PillarHorizontalMovement.InitialScreenPosition;
            
            var pillar = GetComponentInChildren<CubesPillar>();
            
            pillar.SetMovementParameters(ref horizontalMovement);
            pillar.SetPillarBlockPrefab(pillarBlockPrefab);
            
            pillar.CreateEntityIn(world);
        }

        private void OnDisable()
        {
            _entity.SafeDestroy();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TurningZoneTrigger turningZone))
            {
                StartCircularMovement(turningZone.GetMovementData());
            }

            if (other.TryGetComponent(out FinishLine _))
            {
                _finishReached = true;
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

        public void SetScoreMultiplier(int newMultiplier)
        {
            ScoreMultiplier = newMultiplier;
        }
        
        public void MarkDead()
        {
            StopMovingForward();

            if (_finishReached)
            {
                OnLevelCompleted?.Invoke();
                return;
            }
            
            OnDied?.Invoke();
        }
    }
}
