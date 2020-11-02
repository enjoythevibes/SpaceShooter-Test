using enjoythevibes.PlayerDataManager;
using UnityEngine;
using UnityEngine.UI;

namespace enjoythevibes.UI.Map.LevelVertexButton
{    
    namespace enjoythevibes.UI.Map.LevelVertexButton
    {
        public class LevelVertexButtonEntity : MonoBehaviour, ILevelVertexButtonEntity
        {
            [SerializeField] private Image buttonIcon = default;
            private int buttonIndex = default;
            private IPlayerDataManagerEntity playerDataManagerEntity;
            private LevelVertexButtonPressEventArg levelVertexButtonPressEventArg;

            private void Awake()
            {
                EventsManager.AddListener<LevelVertexButtonUpdateEventType>(OnLevelVertexButtonUpdate);
            }

            private void OnDestroy()
            {
                EventsManager.RemoveListener<LevelVertexButtonUpdateEventType>(OnLevelVertexButtonUpdate);
            }

            public void InitButton(IPlayerDataManagerEntity playerDataManagerEntity, int buttonIndex)
            {
                this.playerDataManagerEntity = playerDataManagerEntity;
                this.buttonIndex = buttonIndex;
                levelVertexButtonPressEventArg = new LevelVertexButtonPressEventArg(buttonIndex);
                GetComponent<Button>().onClick.AddListener(() => 
                {
                    EventsManager.CallEvent<LevelVertexButtoPressEventType>(levelVertexButtonPressEventArg);
                });
            }

            private void SetOpened()
            {
                buttonIcon.color = Color.white;
            }

            private void SetClosed()
            {
                buttonIcon.color = Color.red;
            }

            private void SetInProgess()
            {
                buttonIcon.color = Color.green;
            }

            private void OnLevelVertexButtonUpdate()
            {
                var playerData = playerDataManagerEntity.PlayerData;
                var levelData = playerData.GetLevelData(buttonIndex);
                switch (levelData.LevelState)
                {
                    case PlayerData.LevelData.LevelStateEnum.Opened:
                        SetOpened();
                        break;
                    case PlayerData.LevelData.LevelStateEnum.Closed:
                        SetClosed();
                        break;
                    case PlayerData.LevelData.LevelStateEnum.InProgress:
                        SetInProgess();
                        break;
                }
            }
        }
    }
}