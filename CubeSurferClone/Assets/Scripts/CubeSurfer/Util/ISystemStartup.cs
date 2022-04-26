using Leopotam.Ecs;

namespace CubeSurfer.Util
{
    public interface ISystemStartup
    {
        public void AddSystemsTo(EcsSystems systems);
    }
}