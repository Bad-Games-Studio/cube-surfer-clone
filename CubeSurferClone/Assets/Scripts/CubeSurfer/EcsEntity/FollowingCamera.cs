using System;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class FollowingCamera : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Camera.Settings settings;
        
        private Leopotam.Ecs.EcsEntity _entity;

        
        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Camera.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var settingsRef = ref _entity.Get<EcsComponent.Camera.Settings>();
            settingsRef = settings;
        }

        public void SetTarget(Transform target)
        {
            settings.target = target;
        }

        private void OnDisable()
        {
            _entity.SafeDestroy();
        }
    }
}
