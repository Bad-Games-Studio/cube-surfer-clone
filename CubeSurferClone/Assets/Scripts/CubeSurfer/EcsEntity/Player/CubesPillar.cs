using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using PlayerMain = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.EcsEntity.Player
{
    public class CubesPillar : MonoBehaviour, IEcsWorldEntity
    {
        [SerializeField] private EcsComponent.Player.CubesPillar.HorizontalMovement horizontalMovement;
        
        [SerializeField] private GameObject pillarBlockPrefab;
        
        private int BlocksAmount { get; set; }

        private PlayerMain _player;
        
        private Leopotam.Ecs.EcsEntity _entity;
        private EcsWorld _ecsWorld;

        private void Awake()
        {
            _player = transform.parent.GetComponent<PlayerMain>();
            BlocksAmount = 0;
        }

        private void Start()
        {
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

        private void OnDisable()
        {
            _entity.SafeDestroy();
        }

        public void AddPillarBlock()
        {
            var t = transform;
            var position = t.position;
            position.y += BlocksAmount * pillarBlockPrefab.transform.localScale.y;
            
            var instance = Instantiate(pillarBlockPrefab, position, t.localRotation);
            instance.transform.parent = transform;
            
            var pillarBlock = instance.GetComponent<PillarBlock>();
            pillarBlock.CreateEntityIn(_ecsWorld);

            ++BlocksAmount;
        }

        public void DecrementBlocksCounter()
        {
            --BlocksAmount;
            if (BlocksAmount == 0)
            {
                _player.MarkDead();
            }
        }
    }
}