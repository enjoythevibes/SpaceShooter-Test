using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace enjoythevibes.UI.Map
{
    public class MapPath : MonoBehaviour
    {
        private IMapEntity mapEntity;
        [SerializeField] private GameObject lineRendererPrefab = default;

        private void Awake()
        {
            mapEntity = GetComponent<IMapEntity>();
        }

        private void Start()
        {
            CreatePath();
        }

        private void CreatePath()
        {
            var lineGameObject = Instantiate(lineRendererPrefab, lineRendererPrefab.transform.position, lineRendererPrefab.transform.rotation);
            lineGameObject.GetComponent<RectTransform>().SetParent(GetComponent<ScrollRect>().content.transform);
            var lineRendererComponent = lineGameObject.GetComponent<LineRenderer>();
            lineRendererComponent.positionCount = mapEntity.MapVerticiesCount;
            for (int i = 0; i < mapEntity.MapVerticiesCount; i++)
            {
                lineRendererComponent.SetPosition(i, mapEntity.GetMapVertex(i).position);
            }
        }
    }
}