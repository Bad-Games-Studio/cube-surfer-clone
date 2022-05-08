using System;
using System.IO;
using CubeSurfer.GameSave.Version;
using UnityEngine;

namespace CubeSurfer.GameSave
{
    public class GameSaveData
    {
        private const int MaxSupportedVersion = 1;
        private static string Path => $"{Application.dataPath}/save.dat";

        private int LastReadVersion { get; set; }
        public int GemsAmount { get; set; }
        
        
        public static readonly GameSaveData DefaultSettings = new GameSaveData
        {
            LastReadVersion = MaxSupportedVersion,
            GemsAmount = 0,
        };
        
        
        public static GameSaveData ReadFromFile()
        {
            var saveData = new GameSaveData();
            
            using var reader = new BinaryReader(File.Open(Path, FileMode.OpenOrCreate));

            var version = reader.ReadInt32OrDefault(MaxSupportedVersion);
            if (version > MaxSupportedVersion)
            {
                Debug.Log("Cannot read the save file: " +
                          $"file version ({version}) is not yet supported (should be {MaxSupportedVersion} or less).");
                return saveData;
            }

            if (version <= 0)
            {
                Debug.Log("Cannot read the save file: " +
                          "file does not exist, or file version is not supported.");
                return saveData;
            }

            var manager = DecideVersionClass(version);
            manager.Read(reader, saveData);

            return saveData;
        }

        public static void SaveToFile(GameSaveData saveData)
        {
            using var writer = new BinaryWriter(File.Open(Path, FileMode.OpenOrCreate));
            
            writer.Write(MaxSupportedVersion);

            var versionedData = DecideVersionClass(saveData.LastReadVersion);
            versionedData.Save(writer, saveData);

            saveData.LastReadVersion = MaxSupportedVersion;
        }

        private static IVersion DecideVersionClass(int version)
        {
            return version switch
            {
                1 => new V1(),
                _ => throw new ArgumentOutOfRangeException(nameof(version), version, 
                    $"Save file version must be between 1 and {MaxSupportedVersion} (inclusively).")
            };
        }
    }
}
