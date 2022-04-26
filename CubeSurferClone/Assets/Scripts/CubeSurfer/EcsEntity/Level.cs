using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class Level : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Level.GenerationSettings generationSettings;
        
        public void CreateEntityIn(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<EcsComponent.Level.Tag.Main>();

            ref var levelGenerationSettings = ref entity.Get<EcsComponent.Level.GenerationSettings>();
            levelGenerationSettings = generationSettings;
        }
    }
}