using System.IO;

namespace CubeSurfer.GameSave
{
    public static class BinaryReaderExtensions
    {
        public static int ReadInt32OrDefault(this BinaryReader reader, int defaultValue)
        {
            try
            {
                return reader.ReadInt32();
            }
            catch
            {
                return defaultValue;
            }
        } 
    }
}