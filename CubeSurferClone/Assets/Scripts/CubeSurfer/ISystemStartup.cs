using Leopotam.Ecs;

namespace CubeSurfer
{
    public interface ISystemStartup
    {
        public void AddSystemsTo(EcsSystems systems);
    }
}