using System.Collections.Generic;
using UnityEngine;

namespace Mukund._Scripts.EventSystem
{
    [CreateAssetMenu(menuName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListner> listeners = new List<GameEventListner>();

        public void TriggerEvent(Component sender, object data)
        {
            for(int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventTriggered(sender, data);
            }
        }

        //Managing Listners
    
        //Register Listeners if not present
        public void RegisterListener(GameEventListner listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        //Unregister Listeners from the list
        public void UnregisterListener(GameEventListner listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    
    }
}
