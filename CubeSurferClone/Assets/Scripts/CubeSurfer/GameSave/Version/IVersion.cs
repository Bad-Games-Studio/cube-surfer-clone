using System.IO;

namespace CubeSurfer.GameSave.Version
{
    public interface IVersion
    {
        public void Read(BinaryReader reader, ref GameSaveData saveData);

        public void Save(BinaryWriter writer, ref GameSaveData saveData);
    }
}