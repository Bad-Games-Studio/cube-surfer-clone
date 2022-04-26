using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.EcsEntity
{
    public class Level : MonoBehaviour, IEcsWorldEntity
    {
        public void CreateEntityIn(EcsWorld world)
        {
            var entity = world.NewEntity();
            //entity.Get<Component.Level.PlatformTag>(); wut
        }
    }
}