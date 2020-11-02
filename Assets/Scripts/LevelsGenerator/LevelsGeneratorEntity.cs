using enjoythevibes.PlayerDataManager;
using enjoythevibes.UI.Map;
using enjoythevibes.UI.Map.LevelVertexButton;
using UnityEngine;

namespace enjoythevibes.LevelsGenerator
{
    public class LevelsGeneratorEntity : MonoBehaviour/* , ILevelsGeneratorEntity */
    {
        [SerializeField] private GameObject mapEntityGO = default;
        [SerializeField] private GameObject playerDataManagerEntityGO = default;

        private IMapEntity mapEntity;
        private IPlayerDataManagerEntity playerDataManagerEntity;
        [SerializeField] private LevelsGeneratorConfig levelsGeneratorConfig = default;

        private void InitDependencies()
        {
            mapEntity = mapEntityGO.GetComponentWithInterface<IMapEntity>();
            playerDataManagerEntity = playerDataManagerEntityGO.GetComponentWithInterface<IPlayerDataManagerEntity>();
        }

        private void OnValidate() 
        {
            InitDependencies();
        }

        private void Awake()
        {
            InitDependencies();
        }

        private void Start()
        {
            GenerateOrLoadLevelData();
        }
        
        private void GenerateOrLoadLevelData()
        {
            var playerData = playerDataManagerEntity.PlayerData;
            var levelDataCount = playerData.LevelDataCount;
            for (int i = levelDataCount; i < mapEntity.MapVerticiesCount; i++)
            {
                var asteroidsTypeIndex = Random.Range(0, levelsGeneratorConfig.AsteroidsTypePoolsCount);
                var amountOfAsteroids = Random.Range(levelsGeneratorConfig.MinAmountOfAsteroids, levelsGeneratorConfig.MaxAmountOfAsteroids);
                var asteroidsHP = Random.Range(levelsGeneratorConfig.MinAsteroidsHP, levelsGeneratorConfig.MaxAsteroidsHP);
                var levelState = default(PlayerData.LevelData.LevelStateEnum);
                if (i == levelDataCount)
                    levelState = PlayerData.LevelData.LevelStateEnum.InProgress;
                var levelData = new PlayerData.LevelData(asteroidsTypeIndex, amountOfAsteroids, asteroidsHP, levelState);
                playerData.AddLevelData(levelData);            
            }
            EventsManager.CallEvent<SavePlayerDataEventType>();
            EventsManager.CallEvent<LevelVertexButtonUpdateEventType>();
        }
    }
}