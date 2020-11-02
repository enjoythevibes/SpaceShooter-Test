using enjoythevibes.PlayerDataManager;
using enjoythevibes.UI.GameOver;
using enjoythevibes.UI.Win;
using UnityEngine;

namespace enjoythevibes.Level
{
    public class LevelEntity : MonoBehaviour, ILevelEntity
    {
        [SerializeField] private GameObject playerDataManagerEntityGO = default;

        [SerializeField] private int menuSceneID = 0;
        [SerializeField] private Bounds levelBounds = default;
        private IPlayerDataManagerEntity playerDataManagerEntity;

        public Bounds LevelBounds => levelBounds;

        private void InitDependencies()
        {
            playerDataManagerEntity = playerDataManagerEntityGO.GetComponentWithInterface<IPlayerDataManagerEntity>();
        }

        private void OnValidate()
        {
            InitDependencies();
        }

        private void Awake()
        {
            InitDependencies();
            EventsManager.AddListener<GameOverEventType>(OnGameOver);
            EventsManager.AddListener<GameWinEventType>(OnGameWin);
            EventsManager.AddListener<LoadMenuEventType>(OnLoadMenu);
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<GameOverEventType>(OnGameOver);
            EventsManager.RemoveListener<GameWinEventType>(OnGameWin);
            EventsManager.RemoveListener<LoadMenuEventType>(OnLoadMenu);
        }

        private void OnGameOver()
        {
            EventsManager.CallEvent<ShowGameOverScreenEventType>();
        }

        private void OnGameWin()
        {
            var playerData = playerDataManagerEntity.PlayerData;
            var currentLevel = playerData.GetLevelData(playerData.CurrentLevelIndex);
            int nextLevelIndex = playerData.CurrentLevelIndex + 1;
            if (nextLevelIndex < playerData.LevelDataCount)
            {
                var nextLevel = playerData.GetLevelData(nextLevelIndex);
                if (nextLevel.LevelState == PlayerData.LevelData.LevelStateEnum.Closed)
                {
                    nextLevel.ChangeLevelState(PlayerData.LevelData.LevelStateEnum.InProgress);
                }
            }
            currentLevel.ChangeLevelState(PlayerData.LevelData.LevelStateEnum.Opened);
            EventsManager.CallEvent<SavePlayerDataEventType>();
            EventsManager.CallEvent<ShowWinScreenEventType>();
        }

        private void OnLoadMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(menuSceneID, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(levelBounds.center + transform.position, levelBounds.extents * 2f);    
        }
        #endif
    }
}