using UnityEngine;

namespace enjoythevibes.UI.GameOver
{
    public class GameOverScreenEntity : MonoBehaviour
    {
        [SerializeField] private float gameOverScreenTime = 3f;
        private float gameOverScreenTimer;

        private void Awake()
        {
            EventsManager.AddListener<ShowGameOverScreenEventType>(OnShowGameOverScreen);
            HideGameOverScreen();
        }

        private void OnDestroy() 
        {
            EventsManager.RemoveListener<ShowGameOverScreenEventType>(OnShowGameOverScreen);    
        }

        private void OnShowGameOverScreen()
        {
            gameObject.SetActive(true);
            gameOverScreenTimer = 0f;
        }

        private void HideGameOverScreen()
        {
            gameObject.SetActive(false);
        }

        private void LoadMenu()
        {
            EventsManager.CallEvent<Level.LoadMenuEventType>();
        }

        private void Update()
        {
            gameOverScreenTimer += Time.deltaTime;
            if (gameOverScreenTimer > gameOverScreenTime)
            {
                HideGameOverScreen();
                LoadMenu();
            }
        }
    }
}