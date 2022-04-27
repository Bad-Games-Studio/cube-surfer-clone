using System;
using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class Player : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Player.Movement movement;
        
        public void CreateEntityIn(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<EcsComponent.Player.Tag>();

            ref var transformRef = ref entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var rigidBodyRef = ref entity.Get<EcsComponent.RigidbodyRef>();
            rigidBodyRef.Rigidbody = transform.GetComponent<Rigidbody>();

            ref var mov = ref entity.Get<EcsComponent.Player.Movement>();
            mov = movement;
        }
    }
}
