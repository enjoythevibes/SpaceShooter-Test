using UnityEngine;

namespace enjoythevibes.Helper
{
    [RequireComponent(typeof(Camera))]
    public class AutoRatioSize : MonoBehaviour
    {
        [SerializeField]
        private Vector2 aspectRatio = new Vector2(16.0f, 9.0f);

        #if (UNITY_STANDALONE && !UNITY_EDITOR)
        private int lastWidth;
        private int lastHeight;
        #endif

        private void Start()
        {
            SetViewport();
        }

        #if (UNITY_STANDALONE && !UNITY_EDITOR)
        private void FixedUpdate() 
        {
            if (lastWidth != Screen.width || lastHeight != Screen.height)
            {
                lastWidth = Screen.width;
                lastHeight = Screen.height;
                SetViewport();
            }    
        }
        #endif

        private void SetViewport()
        {
            var camera = GetComponent<Camera>();
            var targetAspectFactor = aspectRatio.x / aspectRatio.y;
            var screenAspectFactor = (float)Screen.width / (float)Screen.height;

            var scaleHeightFactor = screenAspectFactor / targetAspectFactor;

            if (scaleHeightFactor < 0.999f)
            {
                var rect = camera.rect;
                rect.width = 1.0f;
                rect.height = scaleHeightFactor;
                rect.x = 0;
                rect.y = (1.0f - scaleHeightFactor) / 2.0f;
                camera.rect = rect;
            }
            else
            if (scaleHeightFactor > 1.01f)
            {
                var scaleWidthFactor = 1.0f / scaleHeightFactor;
                var rect = camera.rect;
                rect.width = scaleWidthFactor;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidthFactor) / 2.0f;
                rect.y = 0;
                camera.rect = rect;
            }
        }
    }
}