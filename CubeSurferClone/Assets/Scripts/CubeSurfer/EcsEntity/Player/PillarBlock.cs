using CubeSurfer.CollisionTag;
using CubeSurfer.Util;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity.Player
{
    public class PillarBlock : MonoBehaviour, IEcsWorldEntity
    {
        private Leopotam.Ecs.EcsEntity _entity;

        private const float VerticalPositionEpsilon = 0.5f;
        
        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.PillarBlock.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var rigidbodyRef = ref _entity.Get<EcsComponent.RigidbodyRef>();
            rigidbodyRef.Rigidbody = transform.GetComponent<Rigidbody>();
        }

        private void OnDestroy()
        {
            if (_entity.IsWorldAlive())
            {
                _entity.Destroy();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out CollectiblePillarBlock block))
            {
                return;
            }
            
            ref var blockCollectedEvent = ref _entity.Get<EcsComponent.Player.PillarBlock.BlockCollectedEvent>();
            blockCollectedEvent.block = block;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var otherObject = collision.transform;
            if (!otherObject.TryGetComponent(out WallBlock _))
            {
                return;
            }
            
            if (!CollisionMagic.CollidesWithSides(transform, collision))
            {
                return;
            }
            
            var isSameYPosition = Mathf.Abs(transform.position.y - otherObject.position.y) < VerticalPositionEpsilon;
            if (!isSameYPosition)
            {
                return;
            }

            ref var collisionEvent = ref _entity.Get<EcsComponent.Player.PillarBlock.WallCollisionEvent>();
            collisionEvent.wall = otherObject;
        }
    }
}