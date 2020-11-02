using UnityEngine;

namespace enjoythevibes.SpaceShip
{
    public class SpaceShipAnimation : MonoBehaviour/* , ISpaceShipAnimation */
    {
        private ISpaceShipEntity shipEntity;
        private ISpaceShipMovement spaceShipMovement;

        private void Awake()
        {
            shipEntity = GetComponent<ISpaceShipEntity>();
            spaceShipMovement = GetComponent<ISpaceShipMovement>();
        }

        private void Update()
        {
            var shipAngle = Functions.Math.Lerp(-1f, 1f, 30f, -30f, spaceShipMovement.HorizontalMovementDirection);
            var shipRotation = Quaternion.Euler(0f, 0f, shipAngle);
            shipEntity.SpaceShipTransform.rotation = shipRotation;
        }
    }
}