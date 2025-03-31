using Mukund._Scripts.EventSystem;
using UnityEngine;

namespace Mukund._Scripts
{
    public class SanityChanger : MonoBehaviour
    {
        public GameEvent onCharSanityChanged;
        public float sanityChange;

        private void OnTriggerEnter(Collider other)
        {
        
            onCharSanityChanged.TriggerEvent(other, sanityChange);
        }
    }
}
