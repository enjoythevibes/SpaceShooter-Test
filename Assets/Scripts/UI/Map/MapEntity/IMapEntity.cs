using UnityEngine;

namespace enjoythevibes.UI.Map
{    
    public interface IMapEntity
    {
        int MapVerticiesCount { get; }
        RectTransform GetMapVertex(int index);
    }
}