using enjoythevibes.Level;
using enjoythevibes.UI.Game.LivesLeftField;
using UnityEngine;

namespace enjoythevibes.SpaceShip
{
    public class SpaceShipEntity : MonoBehaviour, ISpaceShipEntity, IDamagable
    {
        [SerializeField] private Vector3 spaceShipSize = new Vector3(0.5f, 0.5f, 1f);
        [SerializeField] private int livesLeft = 3;
        private LivesLeftChangeTextEventArg leftChangeTextEventArg;

        public Transform SpaceShipTransform { private set; get; }
        public Vector3 SpaceShipSize => spaceShipSize;
        public int LivesLeft => livesLeft;

        private void Awake()
        {
            SpaceShipTransform = transform;
            leftChangeTextEventArg = new LivesLeftChangeTextEventArg();
        }

        private void Start()
        {
            SendLivesChangeEvent();
        }

        private void SendLivesChangeEvent()
        {
            leftChangeTextEventArg.LivesLeft = livesLeft;
            EventsManager.CallEvent<LivesLeftChangeTextEventType>(leftChangeTextEventArg);
        }

        public void Damage(float amount)
        {
            livesLeft--;
            SendLivesChangeEvent();
            if (livesLeft == 0)
            {
                GameOverState();
            }
        }

        private void GameOverState()
        {
            gameObject.SetActive(false);
            EventsManager.CallEvent<GameOverEventType>();
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, spaceShipSize * 2f);
        }
        #endif
    }
}