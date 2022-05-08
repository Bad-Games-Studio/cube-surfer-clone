using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using LevelGenerationSettings = CubeSurfer.EcsComponent.Level.GenerationSettings;

namespace CubeSurfer.EcsSystemsInitialization.Subsystem
{
    public class Level : MonoBehaviour, ISystemStartup
    {
        public void AddSystemsTo(EcsSystems systems, EcsSystems fixedUpdateSystems)
        {
            systems
                .Add(new EcsSystem.Level.Generation())
                .OneFrame<LevelGenerationSettings>();
        }
    }
}