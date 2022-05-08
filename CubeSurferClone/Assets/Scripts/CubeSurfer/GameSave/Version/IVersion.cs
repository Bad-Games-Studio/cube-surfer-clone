using System.IO;

namespace CubeSurfer.GameSave.Version
{
    public interface IVersion
    {
        public void Read(BinaryReader reader, GameSaveData saveData);

        public void Save(BinaryWriter writer, GameSaveData saveData);
    }
}