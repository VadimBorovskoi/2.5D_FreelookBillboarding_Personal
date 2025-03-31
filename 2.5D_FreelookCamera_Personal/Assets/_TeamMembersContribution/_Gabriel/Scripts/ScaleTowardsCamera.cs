using UnityEngine;

namespace _Gabriel.Scripts
{
    public class ScaleTowardsCamera : MonoBehaviour
    {
        public Camera mainCamera;
        public float flattenFactor = 0.5f; // Adjust this to control the degree of flattening

        private Vector3 originalScale;

        void Start()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;

            originalScale = transform.localScale;
        }

        void Update()
        {
            if (mainCamera == null)
                return;

            // Calculate the direction from the object to the camera
            Vector3 cameraToObject = mainCamera.transform.position - transform.position;

            // Project the direction vector onto the object's local xz-plane
            Vector3 localXZ = transform.InverseTransformDirection(cameraToObject);
            localXZ.y = 0;
            cameraToObject = transform.TransformDirection(localXZ);

            // Calculate the scale factor based on the direction vector
            float scaleFactor = Mathf.Pow(Mathf.Abs(Vector3.Dot(transform.forward, cameraToObject.normalized)), flattenFactor);

            // Apply the scale factor to the object's scale
            transform.localScale = originalScale * scaleFactor;
        }
    }
}
