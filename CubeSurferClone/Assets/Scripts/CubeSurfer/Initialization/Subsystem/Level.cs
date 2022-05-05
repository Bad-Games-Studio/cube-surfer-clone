using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization.Subsystem
{
    public class Level : MonoBehaviour, ISystemStartup
    {
        [SerializeField] private EcsEntity.Level level;
        
        public void AddSystemsTo(EcsSystems systems, EcsSystems fixedUpdateSystems)
        {
            systems
                .Add(new EcsSystem.Level.InitSystem())
                .Add(new EcsSystem.Level.Generation())
                .Inject(level);
        }
    }
}