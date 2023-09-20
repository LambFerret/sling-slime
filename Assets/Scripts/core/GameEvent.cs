using System.Collections.Generic;
using UnityEngine;

namespace core
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent", order = 0)]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners;

        public void Raise(Component sender, object data)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(sender, data);
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}