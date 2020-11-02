using UnityEngine;
using enjoythevibes.Pool;

namespace enjoythevibes.LevelsGenerator
{
    [CreateAssetMenu(fileName = "LevelsGeneratorConfig", menuName = "SpaceShooter-Test/LevelsGeneratorConfig", order = 101)]
    public class LevelsGeneratorConfig : ScriptableObject
    {
        [SerializeField] private PoolTemplateScriptableObject[] asteroidsTypePools = default;
        [SerializeField] private int minAmountOfAsteroids = 10;
        [SerializeField] private int maxAmountOfAsteroids = 30;
        [SerializeField] private float minAsteroidsHP = 10f;
        [SerializeField] private float maxAsteroidsHP = 100f;

        public int AsteroidsTypePoolsCount => asteroidsTypePools.Length;
        public PoolTemplateScriptableObject GetAsteroidsTypePool(int index)
        {
            return asteroidsTypePools[index];
        }
        public int MinAmountOfAsteroids => minAmountOfAsteroids;
        public int MaxAmountOfAsteroids => maxAmountOfAsteroids;
        public float MinAsteroidsHP => minAsteroidsHP;
        public float MaxAsteroidsHP => maxAsteroidsHP;
    }
}