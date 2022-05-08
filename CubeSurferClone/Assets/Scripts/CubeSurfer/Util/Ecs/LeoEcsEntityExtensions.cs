using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Util.Ecs
{
    public static class LeoEcsEntityExtensions
    {
        public static void SafeDestroy(this Leopotam.Ecs.EcsEntity entity)
        {
            if (entity.IsWorldAlive())
            {
                entity.Destroy();
            }
        }
    }
}