using TMPro;
using UnityEngine;

namespace Mukund._Scripts
{
    public class UI_SanityChanger : MonoBehaviour
    {
        public TextMeshProUGUI sanityText;
        public void UpdateSanityUI(Component sender, object currentSanity)
        {
            string containerName = gameObject.transform.parent.parent.name;
            if (containerName.Contains(sender.name))
            {
                sanityText.text = currentSanity.ToString();
            }
        }
    }
}
