using UnityEngine;

namespace enjoythevibes.Asteroid
{
    public interface IAsteroidEntity
    {
        float HP { get; }
        Transform AsteroidTransform { get; }
        Vector3 AsteroidSize { get; }
        void SetHP(float amount);
        void SetLevelBounds(Bounds bounds);
    }
}