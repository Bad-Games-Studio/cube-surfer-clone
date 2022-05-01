using Leopotam.Ecs;

namespace CubeSurfer.EcsSystem.Player.Main
{
    public class InitSystem : IEcsInitSystem
    {
        private EcsEntity.Player.Main _player;
        private EcsWorld _world;
    
        public void Init()
        {
            _player.CreateEntityIn(_world);

            var pillar = _player.GetComponentInChildren<EcsEntity.Player.CubesPillar>();
            pillar.CreateEntityIn(_world);
        }
    }
}