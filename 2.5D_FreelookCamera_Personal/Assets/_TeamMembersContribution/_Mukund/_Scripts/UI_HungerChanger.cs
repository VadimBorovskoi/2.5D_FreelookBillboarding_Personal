using TMPro;
using UnityEngine;

namespace Mukund._Scripts
{
    public class UI_HungerChanger : MonoBehaviour
    {
        public TextMeshProUGUI hungerText;
        private float _currentHungerFloat;
        public void UpdateHungerUI(Component sender, object currentHunger)
        {
            
            string containerName = gameObject.transform.parent.parent.name;
            _currentHungerFloat = (float)currentHunger;
            if (containerName.Contains(sender.name))
            {
                hungerText.text = _currentHungerFloat.ToString("F" + 0);
            }
        }
    }
}
