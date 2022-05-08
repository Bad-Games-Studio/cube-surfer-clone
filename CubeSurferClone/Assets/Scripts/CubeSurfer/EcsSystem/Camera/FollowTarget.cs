using CubeSurfer.EcsComponent;
using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;
using CameraTag = CubeSurfer.EcsComponent.Camera.Tag;
using MovementSettings = CubeSurfer.EcsComponent.Camera.Settings;

namespace CubeSurfer.EcsSystem.Camera
{
    public class FollowTarget : IEcsRunSystem
    {
        private EcsFilter<CameraTag, TransformRef, MovementSettings> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var transformRef = ref entity.Get<TransformRef>();
                ref var settings = ref entity.Get<MovementSettings>();
                HandleMovement(transformRef.Transform, ref settings);
            }
        }


        private static void HandleMovement(Transform camera, ref MovementSettings settings)
        {
            var target = settings.target;

            var cameraPosition = MovementMagic.NewPositionFromOffset(target, settings.offset);
            var lookAtPoint    = MovementMagic.NewPositionFromOffset(target, settings.lookAtOffset);

            camera.position = cameraPosition;
            camera.rotation = Quaternion.LookRotation(lookAtPoint - cameraPosition);
        }
    }
}