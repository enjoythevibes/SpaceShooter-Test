using System.Collections.Generic;
using enjoythevibes.Level;
using enjoythevibes.PlayerDataManager;
using enjoythevibes.UI.Map.LevelVertexButton;
using UnityEngine;

namespace enjoythevibes.UI.Map
{
    public class MapEntity : MonoBehaviour, IMapEntity
    {
        [SerializeField] private int levelSceneIndex = default;
        [SerializeField] private GameObject playerDataManagerEntityGO = default;
        [SerializeField] private List<RectTransform> mapVertices = default;
        private IPlayerDataManagerEntity playerDataManagerEntity;

        public int MapVerticiesCount => mapVertices.Count;
        public RectTransform GetMapVertex(int index) => mapVertices[index];

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
            EventsManager.AddListener<LevelVertexButtoPressEventType>(LevelVertexButtonPressed);
            EventsManager.AddListener<LoadLevelEventType>(OnLoadLevel);
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<LevelVertexButtoPressEventType>(LevelVertexButtonPressed);
            EventsManager.RemoveListener<LoadLevelEventType>(OnLoadLevel);
        }

        private void OnLoadLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelSceneIndex, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        private void Start()
        {
            for (int i = 0; i < MapVerticiesCount; i++)
            {
                mapVertices[i].GetComponent<ILevelVertexButtonEntity>().InitButton(playerDataManagerEntity, i);
            }
        }

        private void LevelVertexButtonPressed(IEventArgument arg)
        {
            var argument = arg as LevelVertexButtonPressEventArg;
            var playerData = playerDataManagerEntity.PlayerData;
            var levelDataState = playerData.GetLevelData(argument.ButtonIndex).LevelState;
            if (levelDataState == PlayerData.LevelData.LevelStateEnum.Opened || levelDataState == PlayerData.LevelData.LevelStateEnum.InProgress)
            {
                playerData.CurrentLevelIndex = argument.ButtonIndex;
                EventsManager.CallEvent<SavePlayerDataEventType>();
                EventsManager.CallEvent<LoadLevelEventType>();
            }
        }
    }
}