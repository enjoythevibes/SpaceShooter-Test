using UnityEngine;
using enjoythevibes.Pool;
using enjoythevibes.Level;

namespace enjoythevibes.Asteroid
{
    public class AsteroidEntity : MonoBehaviour, IAsteroidEntity, IDamagable
    {
        [SerializeField] private PoolTemplateScriptableObject asteroidPoolTemplate = default;
        [SerializeField] private float hp = 100f;
        [SerializeField] private Vector3 asteroidSize = Vector3.one;
        private Bounds levelBounds;

        public Transform AsteroidTransform { private set; get; }
        public Vector3 AsteroidSize => asteroidSize;
        public float HP => hp;

        private void Awake()
        {
            AsteroidTransform = transform;
            EventsManager.AddListener<GameOverEventType>(DoDestroy);
        }

        private void OnDestroy() 
        {
            EventsManager.RemoveListener<GameOverEventType>(DoDestroy);    
        }

        public void SetHP(float amount)
        {
            hp = amount;
        }

        public void SetLevelBounds(Bounds bounds)
        {
            levelBounds = bounds;
        }

        public void Damage(float amount)
        {
            hp -= amount;
            if (hp <= 0f)
            {
                hp = 0f;
                DoDestroy();
            }
        }

        private void DoDestroy()
        {
            EventsManager.CallEvent<AsteroidDestroyEventType>();
            asteroidPoolTemplate.PutToPool(gameObject);    
        }

        private void Update()
        {
            if (AsteroidTransform.position.z + asteroidSize.z < levelBounds.min.z)
            {
                OutOfLevelBound();
            }
        }

        private void OutOfLevelBound()
        {
            DoDestroy();
        }

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, asteroidSize * 2f);    
        }
        #endif
    }
}