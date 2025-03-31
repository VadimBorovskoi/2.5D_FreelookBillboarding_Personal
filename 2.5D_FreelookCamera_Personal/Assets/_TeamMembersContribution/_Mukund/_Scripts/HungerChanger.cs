using Mukund._Scripts.EventSystem;
using UnityEngine;

namespace Mukund._Scripts
{
    public class HungerChanger : MonoBehaviour
    {
        public GameEvent onCharHungerChanged;
        public float hungerChange;

        private void OnTriggerEnter(Collider other)
        {
        
            onCharHungerChanged.TriggerEvent(other, hungerChange);
        }
    }
}
