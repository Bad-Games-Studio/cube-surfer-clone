using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization.Subsystem
{
    public class FollowingCamera : MonoBehaviour, ISystemStartup
    {
        [SerializeField] private EcsEntity.FollowingCamera followingCamera;
        
        public void AddSystemsTo(EcsSystems systems)
        {
            systems.Add(new EcsSystem.Camera.InitSystem())
                .Inject(followingCamera);

            systems.Add(new EcsSystem.Camera.FollowTarget());
        }
    }
}
