using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity.Player
{
    public class CubesPillar : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Player.CubesPillar.HorizontalMovement horizontalMovement;
        
        private Leopotam.Ecs.EcsEntity _entity;
        
        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.CubesPillar.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;
            
            ref var pillarMovementRef = ref _entity.Get<EcsComponent.Player.CubesPillar.HorizontalMovement>();
            pillarMovementRef = horizontalMovement;
        }
    }
}