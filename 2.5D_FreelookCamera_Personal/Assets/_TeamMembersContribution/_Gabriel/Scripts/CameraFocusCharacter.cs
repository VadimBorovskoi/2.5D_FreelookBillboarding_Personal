using UnityEngine;

namespace _Gabriel.Scripts
{
    public class CameraFocusCharacter : MonoBehaviour
    {
        public CharacterSwitcher characterSwitcher; // Reference to the CharacterSwitcher script
        public float rotationSpeed = 5f; // Rotation speed for the camera

        void Update()
        {
            if (characterSwitcher != null && characterSwitcher.characters.Length > 0)
            {
                int activeCharacterIndex = characterSwitcher.activeCharacterIndex; // Access active character index

                // Get the controlled character's position
                Vector3 controlledCharacterPosition = characterSwitcher.characters[activeCharacterIndex].transform.position;

                // Calculate the direction from the camera to the controlled character
                Vector3 cameraToCharacterDirection = controlledCharacterPosition - transform.position;

                // Rotate the camera to face the controlled character
                Quaternion targetRotation = Quaternion.LookRotation(cameraToCharacterDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
