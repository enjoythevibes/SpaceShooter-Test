using System;
using System.Collections.Generic;

namespace enjoythevibes.PlayerDataManager
{
    [Serializable]
    public partial class PlayerData
    {
        private List<LevelData> levelDatas = new List<LevelData>();

        public int CurrentLevelIndex { set; get; }
        public int LevelDataCount => levelDatas.Count;
        public LevelData GetLevelData(int index) => levelDatas[index];
        public void AddLevelData(LevelData levelData) => levelDatas.Add(levelData);
    }
}