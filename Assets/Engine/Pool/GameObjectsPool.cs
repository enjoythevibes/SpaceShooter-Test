using System.Collections.Generic;
using UnityEngine;

namespace enjoythevibes.Pool
{
    public class GameObjectsPool
    {
        private GameObject objectToPoolPrefab;
        private Stack<GameObject> pooledObjects = new Stack<GameObject>();
        private GameObject gameObjectsPool;

        public GameObjectsPool(GameObject objectToPoolPrefab, int amountToPool, string name, UnityEngine.SceneManagement.Scene scene)
        {
            this.gameObjectsPool = new GameObject("GameObjects Pool " + name);
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(this.gameObjectsPool, scene);
            this.objectToPoolPrefab = objectToPoolPrefab;
            for (int i = 0; i < amountToPool; i++)
            {   
                var objectToPool = MonoBehaviour.Instantiate(objectToPoolPrefab, Vector3.zero, objectToPoolPrefab.transform.rotation, gameObjectsPool.transform);
                objectToPool.name = objectToPoolPrefab.name;
                objectToPool.SetActive(false);
                pooledObjects.Push(objectToPool);
            }
        }

        public GameObject Take() => Take(Vector3.zero, Quaternion.identity);

        public GameObject Take(Vector3 position, Quaternion rotation)
        {
            if (pooledObjects.Count > 0)
            {
                var returnObject = pooledObjects.Pop();
                returnObject.transform.parent = null;
                returnObject.transform.position = position;
                returnObject.transform.rotation = rotation;
                returnObject.SetActive(true);
                return returnObject;
            }
            else
            {
                var returnObject = MonoBehaviour.Instantiate(objectToPoolPrefab, position, rotation);
                returnObject.name = objectToPoolPrefab.name;
                return returnObject;
            }
        }

        public void Put(GameObject objectToPool)
        {
            objectToPool.transform.parent = gameObjectsPool.transform;
            objectToPool.SetActive(false);
            pooledObjects.Push(objectToPool);
        }
    }
}