using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization.Subsystem
{
    public class Player : MonoBehaviour, ISystemStartup
    {
        [SerializeField] private EcsEntity.Player.Main player;
        
        public void AddSystemsTo(EcsSystems systems)
        {
            systems.Add(new EcsSystem.Player.Main.InitSystem())
                .Inject(player);

            systems
                .Add(new EcsSystem.Player.Main.ForwardMovement())
                .Add(new EcsSystem.Player.Main.CircularMovement());
            
            
            AddCubesPillarSystemsTo(systems);
        }

        private void AddCubesPillarSystemsTo(EcsSystems systems)
        {
            systems.Add(new EcsSystem.Player.CubesPillar.HorizontalMovement());
        }
    }
}