using System;
using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity.Player
{
    public class CubesPillar : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Player.CubesPillar.HorizontalMovement horizontalMovement;
        
        [SerializeField] private GameObject pillarBlockPrefab;
        private int _blocksAmount;

        private Leopotam.Ecs.EcsEntity _entity;

        private void Awake()
        {
            _blocksAmount = 1;
        }

        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.CubesPillar.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;
            
            ref var pillarMovementRef = ref _entity.Get<EcsComponent.Player.CubesPillar.HorizontalMovement>();
            pillarMovementRef = horizontalMovement;
        }

        public void AddPillarBlock()
        {
            var t = transform;
            var position = t.position;
            position.y += _blocksAmount * pillarBlockPrefab.transform.localScale.y;
            
            var instance = Instantiate(pillarBlockPrefab, position, t.localRotation);
            instance.transform.parent = transform;
            
            ++_blocksAmount;
        }

        public void DecrementBlocksCounter()
        {
            --_blocksAmount;
        }
    }
}