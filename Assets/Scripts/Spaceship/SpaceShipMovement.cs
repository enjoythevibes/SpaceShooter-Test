using UnityEngine;

namespace enjoythevibes.SpaceShip
{
    public class SpaceShipMovement : MonoBehaviour, ISpaceShipMovement
    {
        [SerializeField] private GameObject LevelEntityGO = default;
        [SerializeField] private float movementSpeed = 5f;
        private ISpaceShipEntity shipEntity;
        private float horizontalMovementDirection;

        private Bounds levelBounds;

        public float HorizontalMovementDirection => horizontalMovementDirection;
        public float MovementSpeed => movementSpeed;

        private void InitDependencies()
        {
            levelBounds = LevelEntityGO.GetComponent<Level.ILevelEntity>().LevelBounds;
        }

        #if UNITY_EDITOR
        private void OnValidate() 
        {
            if (UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(gameObject) != null)
            {
                InitDependencies();
            }
        }
        #endif

        private void Awake() 
        {
            InitDependencies();
            shipEntity = GetComponent<ISpaceShipEntity>();
        }

        private void Update()
        {
            horizontalMovementDirection = PlayerInputSystem.Horizontal;
            var movementVector = new Vector3(horizontalMovementDirection, 0f, 0f) * movementSpeed;
            var spaceShipTransform = shipEntity.SpaceShipTransform;
            spaceShipTransform.position += movementVector * Time.deltaTime;
            spaceShipTransform.position = new Vector3
            (
                Mathf.Clamp(spaceShipTransform.position.x, levelBounds.min.x + (shipEntity.SpaceShipSize.x), levelBounds.max.x - (shipEntity.SpaceShipSize.x)),
                spaceShipTransform.position.y,
                spaceShipTransform.position.z
            );
        }
    }
}