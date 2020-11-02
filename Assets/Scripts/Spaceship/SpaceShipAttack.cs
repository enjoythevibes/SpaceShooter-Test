using UnityEngine;
using enjoythevibes.Pool;

namespace enjoythevibes.SpaceShip
{
    public class SpaceShipAttack : MonoBehaviour/* , ISpaceShipAttack */
    {
        [SerializeField] private PoolTemplateScriptableObject missilePoolTemplate = default;
        [SerializeField] private Transform missileSpawnPoint = default;
        [SerializeField] private float cooldownTime = 0.1f;
        private float cooldownTimer;

        private void Update()
        {
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer < 0f)
                    cooldownTimer = 0f;
            }
            if (PlayerInputSystem.SpaceDown)
            {
                if (cooldownTimer == 0f)
                {
                    Fire();
                    cooldownTimer = cooldownTime;
                }
            }
        }

        private void Fire()
        {
            var missileGameObject = missilePoolTemplate.TakeFromPool(missileSpawnPoint.position, missilePoolTemplate.TemplatePrefab.transform.rotation);
        }
    }
}