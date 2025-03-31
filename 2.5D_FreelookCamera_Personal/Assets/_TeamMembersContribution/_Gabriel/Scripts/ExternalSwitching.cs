using UnityEngine;

namespace _Gabriel.Scripts
{
    public class ExternalSwitching : MonoBehaviour
    {
        public CharacterSwitcher characterSwitcher; // Reference to the CharacterSwitcher script

        void Start()
        {
            // Assuming you want to switch to character 2
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("External switch to 2");
                characterSwitcher.SwitchCharacter(1); //Since it starts at 0, 1 is character 2
            }


        }
    }
}