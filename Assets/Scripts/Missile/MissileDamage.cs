using UnityEngine;

namespace enjoythevibes.Missile
{
    public class MissileDamage : MonoBehaviour/* , IMissileDamage */
    {
        [SerializeField] private Vector3 damageColliderSize = new Vector3(0.32f, 0.32f, 0.75f);
        [SerializeField] private LayerMask damageLayerMask = default;
        private IMissileEntity missileEntity;
        private Vector3 lastPosition;

        private void Awake()
        {
            if (damageLayerMask == default)
            {
                damageLayerMask = LayerMask.GetMask("Damagable");
            }
            missileEntity = GetComponent<IMissileEntity>();
        }

        private void Update()
        {
            var missileTransform = missileEntity.MissileTransform;
            var raycastVector = missileTransform.position - lastPosition;
            var raycastDistance = raycastVector.magnitude;
            if (Physics.BoxCast(lastPosition - raycastVector, damageColliderSize, raycastVector.normalized, out var raycastHit, missileTransform.rotation, raycastDistance * 2f, damageLayerMask))
            {
                var damagableComponent = raycastHit.transform.root.GetComponent<IDamagable>();
                damagableComponent.Damage(10f);
                missileEntity.DestroyMissile();
            }
            lastPosition = missileTransform.position;
        }

        private void OnEnable()
        {
            lastPosition = missileEntity.MissileTransform.position;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, damageColliderSize * 2f);
        }
        #endif
    }
}