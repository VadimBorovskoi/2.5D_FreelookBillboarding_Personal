using UnityEngine;

public class RotateTowardsObject : MonoBehaviour
{
    public Transform targetObject; // Drag and drop the target game object in the Unity Inspector

    // Update is called once per frame
    void Update()
    {
        // Check if target object is assigned
        if (targetObject != null)
        {
            // Get the direction towards the target object
            Vector3 direction = targetObject.position - transform.position;

            // Calculate rotation to look at the target object
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Apply rotation only around the y-axis (upwards)
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
        else
        {
            Debug.LogWarning("Target object not assigned!");
        }
    }
}
