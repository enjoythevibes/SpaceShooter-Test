using UnityEngine;

namespace enjoythevibes.Missile
{
    public class MissileMovement : MonoBehaviour/* , IMissileMovement */
    {
        private IMissileEntity missileEntity;
        [SerializeField] private float movementSpeed = 5f;

        private void Awake()
        {
            missileEntity = GetComponent<IMissileEntity>();
        }

        private void Update()
        {
            missileEntity.MissileTransform.position += missileEntity.MissileTransform.up * movementSpeed * Time.deltaTime;
        }
    }
}