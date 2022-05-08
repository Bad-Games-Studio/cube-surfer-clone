using CubeSurfer.EcsComponent;
using CubeSurfer.EcsComponent.Level;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using LevelGenerationSettings = CubeSurfer.EcsComponent.Level.GenerationSettings;

namespace CubeSurfer.EcsEntity
{
    public class Level : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private int completionReward;
        public int CompletionReward => completionReward;
        
        [SerializeField] private LevelGenerationSettings generationSettings;
        public ref EcsComponent.Level.GenerationSettings GenerationSettings => ref generationSettings;
        
        public void CreateEntityIn(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<Tag>();

            ref var transformRef = ref entity.Get<TransformRef>();
            transformRef.Transform = transform;
            
            ref var levelGenerationSettings = ref entity.Get<LevelGenerationSettings>();
            levelGenerationSettings = generationSettings;
        }
    }
}