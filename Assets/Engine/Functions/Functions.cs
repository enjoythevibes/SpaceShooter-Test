using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoythevibes
{
    public static class Functions
    {
        public struct Math
        {
            public static float Lerp(float x1, float x2, float fx1, float fx2, float x)
            {
                var fx = fx1 + (x - x1) * ((fx2 - fx1)/(x2 - x1));
                return fx;
            }
        }

        public static T GetComponentWithInterface<T>(this GameObject gameObject)
        {
            #if UNITY_EDITOR
            if (gameObject == null)
            {
                Debug.LogError($"{typeof(T).Name}, GameObject is null");
                return default(T);
            }
            #endif
            if (!typeof(T).IsInterface)
            {
                Debug.LogError($"{typeof(T).Name} is not an interface");
            }
            T component = default(T);
            var components = gameObject.GetComponents<Component>();
            foreach (var item in components)
            {
                if (item is T)
                {
                    component = item.GetComponent<T>();
                    break;
                }
            }
            if (component == null)
            {
                Debug.LogError($"{gameObject.name} doesnt contain {typeof(T).Name} interface");
            }
            return component;
        }
    }
}