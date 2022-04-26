using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization
{
    public class Scene : MonoBehaviour
    {
        private EcsSystems _systems;
        private EcsWorld _world;
    
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            var subsystems = GetComponentsInChildren<ISystemStartup>();
            foreach (var subsystem in subsystems)
            {
                subsystem.AddSystemsTo(_systems);
            }

            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}