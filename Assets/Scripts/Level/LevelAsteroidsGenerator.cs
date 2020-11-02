using UnityEngine;
using enjoythevibes.Pool;
using enjoythevibes.Asteroid;
using enjoythevibes.LevelsGenerator;
using enjoythevibes.PlayerDataManager;

namespace enjoythevibes.Level
{
    public class LevelAsteroidsGenerator : MonoBehaviour/* , ILevelAsteroidsGenerator */
    {    
        [SerializeField] private GameObject playerDataManagerEntityGO = default;

        [SerializeField] private LevelsGeneratorConfig levelsGeneratorConfig = default;
        [SerializeField] private Bounds asteroidSpawnArea = default; 
        [SerializeField] private float spawnRate = 1.5f;
        private float spawnRateTimer;
        private ILevelEntity levelEntity;

        private IPlayerDataManagerEntity playerDataManagerEntity;

        private PoolTemplateScriptableObject asteroidsPool;
        private float defaultAsteroidsHP;
        private int leftAmountOfAsteroidsInScene;
        private int leftAmountOfAsteroidsToGenerate;

        private Collider[] overlapTest = new Collider[1];
        private bool isStopGenerate;

        private void InitDependencies()
        {
            playerDataManagerEntity = playerDataManagerEntityGO.GetComponentWithInterface<IPlayerDataManagerEntity>();
            levelEntity = GetComponent<ILevelEntity>();
        }

        private void OnValidate()
        {
            InitDependencies();
        }

        private void Awake()
        {
            InitDependencies();
            EventsManager.AddListener<AsteroidDestroyEventType>(OnAsteroidDestroy);
            EventsManager.AddListener<GameOverEventType>(OnGameOver);
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<AsteroidDestroyEventType>(OnAsteroidDestroy);
            EventsManager.RemoveListener<GameOverEventType>(OnGameOver);
        }

        private void OnGameOver()
        {
            isStopGenerate = true;
        }

        private void Start()
        {
            LoadGeneratorData();
        }
        
        private void LoadGeneratorData()
        {
            var playerData = playerDataManagerEntity.PlayerData;
            var loadedLevel = playerData.GetLevelData(playerData.CurrentLevelIndex);
            leftAmountOfAsteroidsToGenerate = loadedLevel.AmountOfAsteroids;
            asteroidsPool = levelsGeneratorConfig.GetAsteroidsTypePool(loadedLevel.AsteroidsTypeIndex);
            defaultAsteroidsHP = loadedLevel.AsteroidsHP;
        }

        private void OnAsteroidDestroy()
        {
            leftAmountOfAsteroidsInScene--;
            if (leftAmountOfAsteroidsToGenerate == 0 && leftAmountOfAsteroidsInScene == 0 && !isStopGenerate)
            {

                EventsManager.CallEvent<GameWinEventType>();
            }
        }

        private void Update()
        {
            if (isStopGenerate) return;
            spawnRateTimer += Time.deltaTime;
            if (spawnRateTimer > spawnRate)
            {
                if (leftAmountOfAsteroidsToGenerate > 0)
                {
                    GenerateAsteroid();
                }
                spawnRateTimer = 0f;
            }
        }

        private void GenerateAsteroid()
        {
            var asteroidTemplateEntity = asteroidsPool.TemplatePrefab.GetComponent<IAsteroidEntity>();
            var verifyPosition = false;
            var attempts = 0;
            var randomPosX = 0f;
            var randomPosZ = 0f;
            while (!verifyPosition && attempts < 3)
            {
                randomPosX = Random.Range(asteroidSpawnArea.min.x + asteroidTemplateEntity.AsteroidSize.x, asteroidSpawnArea.max.x - asteroidTemplateEntity.AsteroidSize.x);        
                randomPosZ = Random.Range(asteroidSpawnArea.min.z + asteroidTemplateEntity.AsteroidSize.z, asteroidSpawnArea.max.z- asteroidTemplateEntity.AsteroidSize.z);
                var count = Physics.OverlapBoxNonAlloc(new Vector3(randomPosX, asteroidSpawnArea.center.y, randomPosZ), asteroidTemplateEntity.AsteroidSize, overlapTest, asteroidsPool.TemplatePrefab.transform.rotation);
                if (count == 0)
                {
                    verifyPosition = true;
                }
                attempts++;
            }
            if (verifyPosition)
            {
                var asteroidGameObject = asteroidsPool.TakeFromPool(new Vector3(randomPosX, asteroidSpawnArea.center.y, randomPosZ), Quaternion.identity);
                var asteroidEntity = asteroidGameObject.GetComponent<Asteroid.AsteroidEntity>();
                asteroidEntity.SetLevelBounds(levelEntity.LevelBounds);
                asteroidEntity.SetHP(defaultAsteroidsHP);
                leftAmountOfAsteroidsInScene++;
                leftAmountOfAsteroidsToGenerate--;
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(asteroidSpawnArea.center + transform.position, asteroidSpawnArea.extents * 2f);    
        }
        #endif
    }
}