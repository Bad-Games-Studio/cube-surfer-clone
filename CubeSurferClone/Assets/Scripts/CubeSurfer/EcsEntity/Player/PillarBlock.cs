using CubeSurfer.CollisionTag;
using CubeSurfer.EcsComponent;
using CubeSurfer.Util;
using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;
using PlayerMain = CubeSurfer.EcsEntity.Player.Main;
using PillarBlockTag = CubeSurfer.EcsComponent.Player.PillarBlock.Tag;
using TouchedLavaEvent = CubeSurfer.EcsComponent.Player.PillarBlock.TouchedLavaEvent;
using WallCollisionEvent = CubeSurfer.EcsComponent.Player.PillarBlock.WallCollisionEvent;

namespace CubeSurfer.EcsEntity.Player
{
    public class PillarBlock : MonoBehaviour, IEcsWorldEntity
    {
        private Leopotam.Ecs.EcsEntity _entity;

        public void CreateEntityIn(EcsWorld world)
        {
            _entity = world.NewEntity();
            _entity.Get<PillarBlockTag>();

            ref var transformRef = ref _entity.Get<TransformRef>();
            transformRef.Transform = transform;

            ref var rigidbodyRef = ref _entity.Get<RigidbodyRef>();
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
                CollectBlock(block);
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

        private void CollectBlock(CollectiblePillarBlock block)
        {
            if (block.wasTouched)
            {
                return;
            }

            block.wasTouched = true;
            
            Destroy(block.gameObject);
            
            var pillar = GetComponentInParent<CubesPillar>();
            pillar.AddPillarBlock();
        }

        private void FireTouchedLavaEvent()
        {
            _entity.Get<TouchedLavaEvent>();
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

            _entity.Get<WallCollisionEvent>();
        }

        private void SetNewScoreMultiplier(int newMultiplier)
        {
            var player = transform.GetComponentInParent<PlayerMain>();
            player.SetScoreMultiplier(newMultiplier);
        }
    }
}