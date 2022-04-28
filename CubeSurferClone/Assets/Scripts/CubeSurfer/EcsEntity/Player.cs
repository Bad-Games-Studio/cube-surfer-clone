using System;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out CollisionTag.Level.CurvedPlatform _))
            {
                
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.transform.TryGetComponent(out CollisionTag.Level.CurvedPlatform _))
            {
                
            }
        }
    }
}
