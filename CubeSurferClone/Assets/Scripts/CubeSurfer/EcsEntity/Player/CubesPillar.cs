using CubeSurfer.Util.Ecs;
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
        private EcsWorld _ecsWorld;

        private void Start()
        {
            _blocksAmount = 0;
            AddPillarBlock();
        }

        public void CreateEntityIn(EcsWorld world)
        {
            _ecsWorld = world;
            _entity = _ecsWorld.NewEntity();
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
            
            var pillarBlock = instance.GetComponent<PillarBlock>();
            pillarBlock.CreateEntityIn(_ecsWorld);

            ++_blocksAmount;
        }

        public void DecrementBlocksCounter()
        {
            --_blocksAmount;
        }
    }
}