using UnityEngine;

namespace enjoythevibes.SpaceShip
{    
    public class SpaceShipCollision : MonoBehaviour/* , ISpaceShipCollision */
    {
        private IDamagable damagableEntity;
        [SerializeField] private LayerMask damagableLayerMask = default;

        private void Awake() 
        {
            damagableEntity = GetComponent<IDamagable>();    
            if (damagableLayerMask == default)
            {
                damagableLayerMask = LayerMask.GetMask("Damagable");
            }
        }

        private void OnTriggerEnter(Collider other) 
        {
            damagableEntity.Damage(0f);
            if ((1 << other.gameObject.layer & (damagableLayerMask.value)) != 0)
            {
                other.transform.root.GetComponent<IDamagable>().Damage(500f);
            }
        }
    }
}