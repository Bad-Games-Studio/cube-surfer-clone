using CubeSurfer.CollisionTag;
using CubeSurfer.Util;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using PlayerMain = CubeSurfer.EcsEntity.Player.Main;

namespace CubeSurfer.EcsEntity.Player
{
    public class PillarBlock : MonoBehaviour, IEcsWorldEntity
    {
        private Leopotam.Ecs.EcsEntity _entity;

        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<EcsComponent.Player.PillarBlock.Tag>();

            ref var transformRef = ref _entity.Get<EcsComponent.TransformRef>();
            transformRef.Transform = transform;

            ref var rigidbodyRef = ref _entity.Get<EcsComponent.RigidbodyRef>();
            rigidbodyRef.Rigidbody = transform.GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            _entity.SafeDestroy();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollectiblePillarBlock block))
            {
                FireBlockCollectedEvent(block);
            }

            if (other.TryGetComponent(out LavaPool _))
            {
                FireTouchedLavaEvent();
            }
            
            if (other.TryGetComponent(out BonusZone bonusZone))
            {
                SetNewScoreMultiplier(bonusZone.ScoreMultiplier);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var otherObject = collision.transform;
            if (otherObject.TryGetComponent(out WallBlock _))
            {
                FireWallCollisionEvent(otherObject, collision);
            }
        }

        private void FireBlockCollectedEvent(CollectiblePillarBlock block)
        {
            ref var blockCollectedEvent = ref _entity.Get<EcsComponent.Player.PillarBlock.BlockCollectedEvent>();
            blockCollectedEvent.block = block;
        }

        private void FireTouchedLavaEvent()
        {
            _entity.Get<EcsComponent.Player.PillarBlock.TouchedLavaEvent>();
        }
        
        private void FireWallCollisionEvent(Transform wallObject, Collision collision)
        {
            if (!CollisionMagic.CollidesWithSides(transform, collision))
            {
                return;
            }
            
            if (!CollisionMagic.IsWithinYSize(transform, wallObject))
            {
                return;
            }

            ref var collisionEvent = ref _entity.Get<EcsComponent.Player.PillarBlock.WallCollisionEvent>();
            collisionEvent.wall = wallObject;
        }

        private void SetNewScoreMultiplier(int newMultiplier)
        {
            var player = transform.parent.parent.GetComponent<PlayerMain>();
            player.SetScoreMultiplier(newMultiplier);
        }
    }
}