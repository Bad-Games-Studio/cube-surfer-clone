using System;
using CubeSurfer.CollisionTag;
using UnityEngine;

namespace CubeSurfer.EcsEntity.Player
{
    public class PillarBlock : MonoBehaviour
    {
        private CubesPillar _pillar;
        private Transform _pillarTransform;
        private Transform _myTransform;
        
        private const float VerticalPositionEpsilon = 0.005f;

        private void Start()
        {
            _pillarTransform = transform.parent;
            _pillar = _pillarTransform.GetComponent<CubesPillar>();

            _myTransform = transform;
        }

        private void FixedUpdate()
        {
            var pillarPosition = _pillarTransform.position;
            _myTransform.position = new Vector3
            {
                x = pillarPosition.x,
                y = _myTransform.position.y,
                z = pillarPosition.z
            };
        }

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
            
            _pillar.AddPillarBlock();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.TryGetComponent(out WallBlock wallBlock))
            {
                return;
            }
            
            var wallBlockPosition = wallBlock.transform.position;
            if (Mathf.Abs(_myTransform.position.y - wallBlockPosition.y) > VerticalPositionEpsilon)
            {
                return;
            }
            
            LoseCube();
        }

        private void LoseCube()
        {
            if (_myTransform.parent != null)
            {
                _pillar.DecrementBlocksCounter();
            }
            _myTransform.parent = null;

            Destroy(this);
        }
    }
}