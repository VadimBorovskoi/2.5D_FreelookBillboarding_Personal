using UnityEngine;
using UnityEngine.Events;

namespace Mukund._Scripts.EventSystem
{
    [System.Serializable]
    public class CustomGameEvent : UnityEvent<Component, object> { }
    public class GameEventListner : MonoBehaviour
    {
        public GameEvent gameEvent;
        public CustomGameEvent response;
   
        private void OnEnable()
        {
            gameEvent.RegisterListener(this);

        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventTriggered(Component sender, object data)
        {
            response.Invoke(sender, data);
        }
    }
}