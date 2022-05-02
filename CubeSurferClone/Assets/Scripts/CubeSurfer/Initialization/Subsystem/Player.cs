using CubeSurfer.Util;
using Leopotam.Ecs;
using UnityEngine;

namespace CubeSurfer.Initialization.Subsystem
{
    public class Player : MonoBehaviour, ISystemStartup
    {
        [SerializeField] private EcsEntity.Player.Main player;
        
        public void AddSystemsTo(EcsSystems systems, EcsSystems fixedUpdateSystems)
        {
            systems.Add(new EcsSystem.Player.Main.InitSystem())
                .Inject(player);

            systems
                .Add(new EcsSystem.Player.Main.ForwardMovement())
                .Add(new EcsSystem.Player.Main.CircularMovement());

            AddCubesPillarSystemsTo(systems, fixedUpdateSystems);
        }

        private static void AddCubesPillarSystemsTo(EcsSystems systems, EcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new EcsSystem.Player.PillarCube.PositionAlignment());
            
            systems
                .Add(new EcsSystem.Player.CubesPillar.HorizontalMovement())
                .Add(new EcsSystem.Player.PillarCube.BlockCollecting())
                .Add(new EcsSystem.Player.PillarCube.WallCollision())
                .OneFrame<EcsComponent.Player.PillarBlock.BlockCollectedEvent>()
                .OneFrame<EcsComponent.Player.PillarBlock.WallCollisionEvent>();
        }
    }
}