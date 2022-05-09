using System;
using System.IO;
using CubeSurfer.GameSave.Version;
using UnityEngine;

namespace CubeSurfer.GameSave
{
    public struct GameSaveData
    {
        private const int MaxSupportedVersion = 2;
        private static string Folder => $"{Application.persistentDataPath}/Saves";
        private static string Path => $"{Folder}/save.dat";

        private int LastReadVersion { get; set; }
        
        public int GemsAmount { get; set; }
        public int LevelIndex { get; set; }


        public static readonly GameSaveData DefaultSettings = new GameSaveData
        {
            LastReadVersion = MaxSupportedVersion,
            GemsAmount = 0
        };


        public void Load()
        {
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
            
            using var reader = new BinaryReader(File.Open(Path, FileMode.OpenOrCreate));
            
            var version = reader.ReadInt32OrDefault(MaxSupportedVersion);
            if (version > MaxSupportedVersion)
            {
                Debug.Log("Cannot read the save file: " +
                          $"file version ({version}) is not yet supported (should be {MaxSupportedVersion} or less).");
                return;
            }

            if (version <= 0)
            {
                Debug.Log("Cannot read the save file: " +
                          "file does not exist, or file version is not supported.");
                return;
            }

            var manager = DecideVersionClass(version);
            manager.Read(reader, ref this);
        }

        public void Save()
        {
            using var writer = new BinaryWriter(File.Open(Path, FileMode.OpenOrCreate));
            
            writer.Write(MaxSupportedVersion);

            var versionedData = DecideVersionClass(LastReadVersion);
            versionedData.Save(writer, ref this);

            LastReadVersion = MaxSupportedVersion;
        }
        
        
        public static GameSaveData LoadFromFile()
        {
            var saveData = DefaultSettings;
            
            saveData.Load();

            return saveData;
        }

        public static void SaveToFile(ref GameSaveData saveData)
        {
            saveData.Save();
        }

        private static IVersion DecideVersionClass(int version)
        {
            return version switch
            {
                1 => new V1(),
                2 => new V2(),
                _ => throw new ArgumentOutOfRangeException(nameof(version), version, 
                    $"Save file version must be between 1 and {MaxSupportedVersion} (inclusively).")
            };
        }
    }
}
