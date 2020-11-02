using UnityEngine;
using enjoythevibes.Pool;

namespace enjoythevibes.Missile
{
    public class MissileEntity : MonoBehaviour, IMissileEntity
    {
        [SerializeField] private PoolTemplateScriptableObject missilePoolTemplate = default;
        [SerializeField] private float lifetime = 3f;
        private float lifeTimer;
        
        public Transform MissileTransform { private set; get; }

        private void Awake()
        {
            MissileTransform = transform;
            EventsManager.AddListener<Level.GameOverEventType>(DestroyMissile);
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<Level.GameOverEventType>(DestroyMissile);    
        }

        private void Update()
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifetime)
            {
                DestroyMissile();
            }
        }
        
        public void DestroyMissile()
        {
            lifeTimer = 0f;
            missilePoolTemplate.PutToPool(gameObject);
        }
    }
}