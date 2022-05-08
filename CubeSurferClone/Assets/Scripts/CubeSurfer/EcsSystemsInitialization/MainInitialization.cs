using System;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystemsInitialization
{
    public class MainInitialization : MonoBehaviour
    {
        public event Action OnSystemInitFinished;
        
        public EcsWorld World { get; private set; }
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;
        
        private void Start()
        {
            World = new EcsWorld();
            _systems = new EcsSystems(World);
            _fixedUpdateSystems = new EcsSystems(World);

            var subsystems = GetComponentsInChildren<ISystemStartup>();
            foreach (var subsystem in subsystems)
            {
                subsystem.AddSystemsTo(_systems, _fixedUpdateSystems);
            }

            _systems.Init();
            _fixedUpdateSystems.Init();
            
            OnSystemInitFinished?.Invoke();
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
            World.Destroy();
        }
    }
}