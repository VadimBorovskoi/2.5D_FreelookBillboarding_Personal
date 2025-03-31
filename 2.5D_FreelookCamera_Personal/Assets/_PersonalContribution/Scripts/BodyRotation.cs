using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    public AnimationCurve accelerationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public Quaternion Rotation => transform.rotation;
    public void RotateTowards(Quaternion target)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target, 5f * Time.fixedDeltaTime);
    }
    public void AutoRotate()
    {
        float nearestAngle = Mathf.Round(transform.eulerAngles.y / 45) * 45;

        // Calculate the angle difference between the current rotation and the nearest angle
        float angleDifference = nearestAngle - transform.eulerAngles.y;

        // Evaluate the acceleration curve to get the rotation speed multiplier
        float accelerationMultiplier = accelerationCurve.Evaluate(Mathf.Abs(angleDifference) / 180f);

        // Calculate the rotation speed
        float speed = rotationSpeed * accelerationMultiplier;

        // Rotate towards the nearest angle
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, nearestAngle, 0), speed * Time.deltaTime);
    }
    
}
