using System;

namespace enjoythevibes.PlayerDataManager
{
    public partial class PlayerData
    {
        [Serializable]
        public class LevelData
        {
            public enum LevelStateEnum
            {
                Closed,
                InProgress,
                Opened
            }
            public int AsteroidsTypeIndex { private set; get; }
            public int AmountOfAsteroids { private set; get; }
            public float AsteroidsHP { private set; get; }
            public LevelStateEnum LevelState { private set; get; }

            public LevelData(int asteroidsTypeIndex, int amountOfAsteroids, float asteroidsHP, LevelStateEnum levelState)
            {
                AsteroidsTypeIndex = asteroidsTypeIndex;
                AmountOfAsteroids = amountOfAsteroids;
                AsteroidsHP = asteroidsHP;
                LevelState = levelState;
            }

            public void ChangeLevelState(LevelStateEnum levelStateEnum)
            {
                LevelState = levelStateEnum;
            }
        }
    }
}