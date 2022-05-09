using Leopotam.Ecs;

namespace CubeSurfer.Util.Ecs
{
    public interface ISystemStartup
    {
        public void AddSystemsTo(EcsSystems systems, EcsSystems fixedUpdateSystems);
    }
}