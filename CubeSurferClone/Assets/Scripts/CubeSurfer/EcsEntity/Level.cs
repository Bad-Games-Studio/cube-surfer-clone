using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class Level : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private int completionReward;
        public int CompletionReward => completionReward;
        
        [SerializeField] private EcsComponent.Level.GenerationSettings generationSettings;
        public ref EcsComponent.Level.GenerationSettings GenerationSettings => ref generationSettings;
        
        public void CreateEntityIn(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<EcsComponent.Level.Tag.Main>();

            ref var levelGenerationSettings = ref entity.Get<EcsComponent.Level.GenerationSettings>();
            levelGenerationSettings = generationSettings;
        }
    }
}