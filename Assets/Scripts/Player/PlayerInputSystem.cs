using UnityEngine;

namespace enjoythevibes
{
    public class PlayerInputSystem : MonoBehaviour
    {
        private static PlayerInputSystem instance;
        public static float Vertical { private set; get; }
        public static float Horizontal { private set; get; }
        public static bool SpaceDown { private set; get; }

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Update() 
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");
            var spaceDown = Input.GetKeyDown(KeyCode.Space);

            Vertical = vertical;
            Horizontal = horizontal;
            SpaceDown = spaceDown;
        }
    }
}