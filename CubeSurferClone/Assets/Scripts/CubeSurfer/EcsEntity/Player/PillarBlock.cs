using System;
using CubeSurfer.CollisionTag;
using UnityEngine;

namespace CubeSurfer.EcsEntity.Player
{
    public class PillarBlock : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out CollectiblePillarBlock block))
            {
                return;
            }

            if (block.wasTouched)
            {
                return;
            }

            block.wasTouched = true;
            Destroy(other.gameObject);
            
            var pillar = transform.parent.GetComponent<CubesPillar>();
            pillar.AddPillarBlock();
        }
    }
}