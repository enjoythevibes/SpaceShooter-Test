using enjoythevibes.Data;
using UnityEngine;

namespace enjoythevibes.PlayerDataManager
{
    public class PlayerDataManagerEntity : MonoBehaviour, IPlayerDataManagerEntity
    {
        private PlayerData playerData;

        public PlayerData PlayerData => playerData;

        private void Awake()
        {
            EventsManager.AddListener<LoadPlayerDataEventType>(OnLoadPlayerData);
            EventsManager.AddListener<SavePlayerDataEventType>(OnSavePlayerData);
        }

        private void Start()
        {
            OnLoadPlayerData();
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<LoadPlayerDataEventType>(OnLoadPlayerData);
            EventsManager.RemoveListener<SavePlayerDataEventType>(OnSavePlayerData);
        }

        private void OnLoadPlayerData()
        {
            playerData = DataSaver.LoadPlayerData();
        }

        private void OnSavePlayerData()
        {
            DataSaver.SaveData(playerData);
        }
    }
}