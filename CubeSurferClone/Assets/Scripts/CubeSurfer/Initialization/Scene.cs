using System;
using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization
{
    public class Scene : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;
        
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);

            var subsystems = GetComponentsInChildren<ISystemStartup>();
            foreach (var subsystem in subsystems)
            {
                subsystem.AddSystemsTo(_systems, _fixedUpdateSystems);
            }

            _systems.Init();
            _fixedUpdateSystems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}