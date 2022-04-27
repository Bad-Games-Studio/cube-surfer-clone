using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization.Subsystem
{
    public class Player : MonoBehaviour, ISystemStartup
    {
        [SerializeField] private EcsEntity.Player player;
        
        public void AddSystemsTo(EcsSystems systems)
        {
            systems.Add(new EcsSystem.Player.InitSystem())
                .Inject(player);

            systems.Add(new EcsSystem.Player.Movement());
        }
    }
}