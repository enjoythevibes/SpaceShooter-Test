using UnityEngine;

namespace enjoythevibes.UI.Win
{
    public class WinScreenEntity : MonoBehaviour
    {
        [SerializeField] private float winScreenTime = 3f;
        private float winScreenTimer;

        private void Awake()
        {
            EventsManager.AddListener<ShowWinScreenEventType>(OnShowWinScreen);
            HideWinScreen();
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<ShowWinScreenEventType>(OnShowWinScreen);
        }

        private void OnShowWinScreen()
        {
            gameObject.SetActive(true);
            winScreenTimer = 0f;
        }

        private void HideWinScreen()
        {
            gameObject.SetActive(false);
        }

        private void LoadMenu()
        {
            EventsManager.CallEvent<Level.LoadMenuEventType>();
        }

        private void Update()
        {
            winScreenTimer += Time.deltaTime;
            if (winScreenTimer > winScreenTime)
            {
                HideWinScreen();
                LoadMenu();
            }
        }
    }
}