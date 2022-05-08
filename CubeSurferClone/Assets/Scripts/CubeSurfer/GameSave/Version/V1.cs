using System.IO;

namespace CubeSurfer.GameSave.Version
{
    public class V1 : IVersion
    {
        public void Read(BinaryReader reader, ref GameSaveData saveData)
        {
            saveData.GemsAmount = reader.ReadInt32OrDefault(GameSaveData.DefaultSettings.GemsAmount);
        }

        public void Save(BinaryWriter writer, ref GameSaveData saveData)
        {
            writer.Write(saveData.GemsAmount);
        }
    }
}