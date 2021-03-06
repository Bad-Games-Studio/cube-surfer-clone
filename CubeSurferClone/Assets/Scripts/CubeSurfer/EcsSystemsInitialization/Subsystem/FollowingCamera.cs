using CubeSurfer.Util.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsSystemsInitialization.Subsystem
{
    public class FollowingCamera : MonoBehaviour, ISystemStartup
    {
        public void AddSystemsTo(EcsSystems systems, EcsSystems fixedUpdateSystems)
        {
            systems.Add(new EcsSystem.Camera.FollowTarget());
        }
    }
}
