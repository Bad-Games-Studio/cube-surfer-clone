using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class FollowingCamera : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Camera.Settings settings;
        
        public void CreateEntityIn(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<EcsComponent.Camera.Tag>();

            ref var transformRef = ref entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var settingsRef = ref entity.Get<EcsComponent.Camera.Settings>();
            settingsRef = settings;
        }
    }
}
