using UnityEngine;

namespace enjoythevibes.Missile
{
    public interface IMissileEntity
    {
        Transform MissileTransform { get; }
        void DestroyMissile();
    }
}