using Mukund._Scripts.EventSystem;
using UnityEngine;

namespace Mukund._Scripts
{
    public class HealthChanger : MonoBehaviour
    {
        public GameEvent onCharHealthChanged;
        public float healthChange;

        private void OnTriggerEnter(Collider other)
        {
        
            onCharHealthChanged.TriggerEvent(other, healthChange);
        }

    }
}
