using Leopotam.Ecs;

namespace CubeSurfer.EcsSystem.Level
{
    public class InitSystem : IEcsInitSystem
    {
        private EcsEntity.Level _level;
        private EcsWorld _world;
    
        public void Init()
        {
            _level.CreateEntityIn(_world);
        }
    }
}