using System.IO;

namespace CubeSurfer.GameSave.Version
{
    public class V2 : IVersion
    {
        private V1 _previous;

        public V2()
        {
            _previous = new V1();
        }
        
        public void Read(BinaryReader reader, ref GameSaveData saveData)
        {
            _previous.Read(reader, ref saveData);

            saveData.LevelIndex = reader.ReadInt32OrDefault(0);
        }

        public void Save(BinaryWriter writer, ref GameSaveData saveData)
        {
            _previous.Save(writer, ref saveData);
            
            writer.Write(saveData.LevelIndex);
        }
    }
}