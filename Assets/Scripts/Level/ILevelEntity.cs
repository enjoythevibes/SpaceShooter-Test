using UnityEngine;

namespace enjoythevibes.Level
{
    public interface ILevelEntity
    {
        Bounds LevelBounds { get; }
    }
}