using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization.Subsystem
{
    public class Level : MonoBehaviour, ISystemStartup
    {
        [SerializeField] private EcsEntity.Level level;
        
        public void AddSystemsTo(EcsSystems systems)
        {
            systems.Add(new EcsSystem.Level.InitSystem())
                .Inject(level);

            systems.Add(new EcsSystem.Level.Generation())
                .Inject(level);
        }
    }
}