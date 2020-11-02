using UnityEngine;

namespace enjoythevibes.Asteroid
{
    public class AsteroidMovement : MonoBehaviour, IAsteroidMovement
    {
        private IAsteroidEntity asteroidEntity;
        [SerializeField] private float movementSpeed = 1f;

        public float MovementSpeed => movementSpeed;

        private void Awake()
        {
            asteroidEntity = GetComponent<IAsteroidEntity>();
        }

        private void Update()
        {
            asteroidEntity.AsteroidTransform.position += Vector3.back * movementSpeed * Time.deltaTime;
        }
    }
}