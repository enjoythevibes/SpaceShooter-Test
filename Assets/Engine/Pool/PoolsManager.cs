using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace enjoythevibes.Pool
{
    public class PoolsManager : MonoBehaviour
    {
        [SerializeField] private List<PoolTemplateScriptableObject> poolTemplates = default;
        private static PoolsManager instance;

        private Dictionary<int, Dictionary<string, GameObjectsPool>> poolsBySceneIndex = new Dictionary<int, Dictionary<string, GameObjectsPool>>();

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                instance = this;
                SceneManager.sceneLoaded += OnSceneLoad;
                SceneManager.sceneUnloaded += OnSceneUnload;
                CreatePools(SceneManager.GetActiveScene());
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
        {
            CreatePools(scene);
        }

        private void OnSceneUnload(Scene scene)
        {
            if (poolsBySceneIndex.ContainsKey(scene.buildIndex))
            {
                poolsBySceneIndex.Remove(scene.buildIndex);
            }
        }

        private void CreatePools(Scene scene)
        {
            for (int i = 0; i < poolTemplates.Count; i++)
            {
                if (scene.buildIndex == poolTemplates[i].RelatedSceneIndex)
                {
                    if (!poolsBySceneIndex.TryGetValue(scene.buildIndex, out var pools))
                    {
                        pools = new Dictionary<string, GameObjectsPool>();
                        poolsBySceneIndex.Add(scene.buildIndex, pools);
                    }
                    if (!pools.ContainsKey(poolTemplates[i].TemplateTagName))
                    {
                        var gameObjectsPool = new GameObjectsPool(poolTemplates[i].TemplatePrefab, poolTemplates[i].AmountToPool, poolTemplates[i].TemplateTagName, scene);
                        pools.Add(poolTemplates[i].TemplateTagName, gameObjectsPool);
                    }
                }
            }
        }

        public static GameObjectsPool GetGameObjectsPool(int sceneIndex, string templateTagName)
        {
            var result = default(GameObjectsPool);
            if (instance.poolsBySceneIndex.TryGetValue(sceneIndex, out var pools))
            {
                if (pools.TryGetValue(templateTagName, out var pool))
                {
                    result = pool;
                }
            }
            return result;
        }
    }
}