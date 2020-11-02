using UnityEngine;

namespace enjoythevibes.Pool
{
    [CreateAssetMenu(fileName = "PoolTemplate", menuName = "enjoythevibes/PoolTemplate", order = 100)]
    public class PoolTemplateScriptableObject : ScriptableObject
    {
        [SerializeField] private string templateTagName = default;
        [SerializeField] private GameObject templatePrefab = default;
        [SerializeField] private int amountToPool = default;
        [SerializeField] private int relatedSceneIndex = 0;
        
        public int RelatedSceneIndex
        {
            get 
            {
                return relatedSceneIndex;
            }
        } 
        public string TemplateTagName => templateTagName;
        public GameObject TemplatePrefab => templatePrefab;
        public int AmountToPool => amountToPool;

        public GameObject TakeFromPool()
        {
            return PoolsManager.GetGameObjectsPool(relatedSceneIndex, templateTagName).Take();
        }

        public GameObject TakeFromPool(Vector3 position, Quaternion rotation)
        {
            return PoolsManager.GetGameObjectsPool(relatedSceneIndex, templateTagName).Take(position, rotation);
        }

        public void PutToPool(GameObject gameObject)
        {
            PoolsManager.GetGameObjectsPool(relatedSceneIndex, templateTagName).Put(gameObject);
        }
    }
}