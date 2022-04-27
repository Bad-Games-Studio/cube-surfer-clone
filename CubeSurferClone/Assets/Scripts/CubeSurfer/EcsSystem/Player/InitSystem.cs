using Leopotam.Ecs;

namespace CubeSurfer.EcsSystem.Player
{
    public class InitSystem : IEcsInitSystem
    {
        private EcsEntity.Player _player;
        private EcsWorld _world;
    
        public void Init()
        {
            _player.CreateEntityIn(_world);
        }
    }
}