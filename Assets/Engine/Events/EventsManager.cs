using System;
using System.Collections.Generic;
using enjoythevibes.EventsManagerCore;
using UnityEngine;

namespace enjoythevibes
{
    public class EventsManager : MonoBehaviour
    {
        private struct ActionEventArg : IEquatable<ActionEventArg>
        {
            public Action<IEventArgument> action;
            public IEventArgument arg;

            public ActionEventArg(Action<IEventArgument> action, IEventArgument arg)
            {
                this.action = action;
                this.arg = arg;
            }

            public bool Equals(ActionEventArg other)
            {
                return ((action == other.action) && (arg == other.arg));
            }
        }
        private static EventsManager instance;
        private static Queue<Action> actionsToCall = new Queue<Action>();
        private static Queue<ActionEventArg> actionsToCallArgument = new Queue<ActionEventArg>();

        private static Dictionary<int, Action> actions = new Dictionary<int, Action>();
        private static Dictionary<int, Action<IEventArgument>> actionsWithArgument = new Dictionary<int, Action<IEventArgument>>();

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public static void AddListener<T>(Action action) where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actions.ContainsKey(key))
            {
                actions[key] += action;
            }
            else
            {
                actions.Add(key, null);
                actions[key] += action;
            }
        }

        public static void AddListener<T>(Action<IEventArgument> action) where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actionsWithArgument.ContainsKey(key))
            {
                actionsWithArgument[key] += action;
            }
            else
            {
                actionsWithArgument.Add(key, null);
                actionsWithArgument[key] += action;
            }
        }
        
        public static void RemoveListener<T>(Action action) where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actions.ContainsKey(key))
            {
                actions[key] -= action;
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Listener with {key} key not found | Remove");
            }
            #endif
        }

        public static void RemoveListener<T>(Action<IEventArgument> action) where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actionsWithArgument.ContainsKey(key))
            {
                actionsWithArgument[key] -= action;
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Listener with {key} key not found | Remove with argument");
            }
            #endif
        }

        public static void CallEventImmediate<T>() where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actions.TryGetValue(key, out var action))
            {
                action.Invoke();
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Listener with {key} key not found | CallEventImmediate");
            }
            #endif
        }

        public static void CallEvent<T>() where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actions.TryGetValue(key, out var action))
            {
                if (!actionsToCall.Contains(action))
                    actionsToCall.Enqueue(action);
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Listener with {key} key not found | Call");
            }
            #endif
        }

        public static void CallEventImmediate<T>(IEventArgument argument) where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actionsWithArgument.TryGetValue(key, out var action))
            {
                action.Invoke(argument);
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Listener with {key} key not found | CallEventImmediate with argument");
            }
            #endif
        }

        public static void CallEvent<T>(IEventArgument argument) where T : struct, IEventType
        {
            var key = EventTypeKeys<T>.EventTypeKey;
            if (actionsWithArgument.TryGetValue(key, out var action))
            {
                var value = new ActionEventArg(action, argument);
                if (!actionsToCallArgument.Contains(value))
                    actionsToCallArgument.Enqueue(value);
                
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Listener with {key} key not found | Call with argument");
            }
            #endif
        }

        private void Update()
        {
            while (actionsToCall.Count > 0)
            {
                var toCall = actionsToCall.Dequeue();
                toCall.Invoke();
            }
            while (actionsToCallArgument.Count > 0)
            {
                var toCall = actionsToCallArgument.Dequeue();
                toCall.action.Invoke(toCall.arg);
            }
        }
    }
}