using Leopotam.Ecs;

namespace CubeSurfer.EcsSystem.Camera
{
    public class InitSystem : IEcsInitSystem
    {
        private EcsEntity.FollowingCamera _followingCamera;
        private EcsWorld _world;
    
        public void Init()
        {
            _followingCamera.CreateEntityIn(_world);
        }
    }
}