using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using enjoythevibes.PlayerDataManager;

namespace enjoythevibes.Data
{
    public static class DataSaver
    {
        private static string GetPath()
        {
            var finalPath = default(string);
            #if !UNITY_EDITOR && UNITY_STANDALONE
            finalPath = Application.dataPath + "/data.enj";
            #endif
            #if UNITY_ANDROID
            finalPath = Application.persistentDataPath + "/data.enj";
            #endif
            #if UNITY_EDITOR
            finalPath = "data.enj";
            #endif
            return finalPath;
        }

        public static void SaveData(PlayerData playerData)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = new FileStream(GetPath(), FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, playerData);
            }
        }

        public static PlayerData LoadPlayerData()
        {
            var playerData = default(PlayerData);
            var path = GetPath();
            if(File.Exists(path))
            {
                var binaryFormatter = new BinaryFormatter();
                using (var fileStream = new FileStream(GetPath(), FileMode.OpenOrCreate))
                {
                    playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
                }
            }
            else
            {
                playerData = new PlayerData();
                SaveData(playerData);
            }
            return playerData;
        }
    }
}