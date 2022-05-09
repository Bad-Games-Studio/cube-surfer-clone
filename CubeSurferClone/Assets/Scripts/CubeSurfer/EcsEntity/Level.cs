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
        [SerializeField] private LevelGenerationSettings generationSettings;

        public int CompletionReward => completionReward;
        public ref LevelGenerationSettings GenerationSettings => ref generationSettings;
        
        
        private Leopotam.Ecs.EcsEntity _entity;
        
        
        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<Tag>();

            ref var transformRef = ref _entity.Get<TransformRef>();
            transformRef.Transform = transform;
            
            ref var levelGenerationSettings = ref _entity.Get<LevelGenerationSettings>();
            levelGenerationSettings = generationSettings;
        }

        private void OnDisable()
        {
            _entity.SafeDestroy();
        }
    }
}