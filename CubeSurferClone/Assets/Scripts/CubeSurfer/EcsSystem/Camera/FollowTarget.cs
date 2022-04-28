using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystem.Camera
{
    public class FollowTarget : IEcsRunSystem
    {
        private EcsFilter<
            EcsComponent.Camera.Tag,
            EcsComponent.TransformRef,
            EcsComponent.Camera.Settings> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                var transformRef = entity.Get<EcsComponent.TransformRef>();
                ref var settings = ref entity.Get<EcsComponent.Camera.Settings>();
                HandleMovement(transformRef.Transform, ref settings);
            }
        }


        private static void HandleMovement(Transform camera, ref EcsComponent.Camera.Settings settings)
        {
            var target = settings.target;

            var cameraPosition = MovementMagic.NewPositionFromOffset(target, settings.offset);
            var lookAtPoint    = MovementMagic.NewPositionFromOffset(target, settings.lookAtOffset);

            camera.position = cameraPosition;
            camera.rotation = Quaternion.LookRotation(lookAtPoint - cameraPosition);
        }
    }
}