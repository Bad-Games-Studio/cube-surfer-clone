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
            var targetPosition = target.position;

            var xAxisLocal = target.right;
            var yAxisLocal = target.up;
            var zAxisLocal = target.forward;

            var cameraPositionOffset =
                settings.offset.x * xAxisLocal +
                settings.offset.y * yAxisLocal +
                settings.offset.z * zAxisLocal;
            var cameraPosition = targetPosition + cameraPositionOffset;
                
            var lookAtOffset =
                settings.lookAtOffset.x * xAxisLocal +
                settings.lookAtOffset.y * yAxisLocal +
                settings.lookAtOffset.z * zAxisLocal;
            var lookAtPoint = targetPosition + lookAtOffset;

            camera.position = cameraPosition;
            camera.rotation = Quaternion.LookRotation(lookAtPoint - cameraPosition);
        }
    }
}