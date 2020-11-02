using UnityEngine;

namespace enjoythevibes.SpaceShip
{    
    public interface ISpaceShipEntity
    {
        Transform SpaceShipTransform { get; }
        Vector3 SpaceShipSize { get; }
        int LivesLeft { get; }
    }
}