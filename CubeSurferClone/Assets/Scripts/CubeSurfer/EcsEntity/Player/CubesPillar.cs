using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using PlayerMain = CubeSurfer.EcsEntity.Player.Main;
using HorizontalMovement = CubeSurfer.EcsComponent.Player.CubesPillar.HorizontalMovement;

namespace CubeSurfer.EcsEntity.Player
{
    public class CubesPillar : MonoBehaviour, IEcsWorldEntity
    {
        private GameObject _pillarBlockPrefab;
        private HorizontalMovement _horizontalMovement;
        
        private int BlocksAmount { get; set; }

        private PlayerMain _player;
        
        private Leopotam.Ecs.EcsEntity _entity;
        private EcsWorld _ecsWorld;

        private Transform _myTransform;

        public void SetMovementParameters(ref HorizontalMovement newParameters)
        {
            _horizontalMovement = newParameters;
        }

        public void SetPillarBlockPrefab(GameObject prefab)
        {
            _pillarBlockPrefab = prefab;
        }
        
        private void Awake()
        {
            _myTransform = transform;
            
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
            
            ref var pillarMovementRef = ref _entity.Get<HorizontalMovement>();
            pillarMovementRef = _horizontalMovement;
        }

        private void OnDisable()
        {
            _entity.SafeDestroy();
        }

        public void AddPillarBlock()
        {
            var position = _myTransform.position;
            if (BlocksAmount > 0)
            {
                position = TopmostCubePosition();
                position.y += _pillarBlockPrefab.transform.localScale.y;
            }
            
            var instance = Instantiate(_pillarBlockPrefab, position, _myTransform.localRotation);
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

        private Vector3 TopmostCubePosition()
        {
            var highestPosition = _myTransform.position;
            for (var i = 0; i < _myTransform.childCount; ++i)
            {
                var childPosition = transform.GetChild(i).position;
                if (childPosition.y > highestPosition.y)
                {
                    highestPosition = childPosition;
                }
            }

            return highestPosition;
        }
    }
}