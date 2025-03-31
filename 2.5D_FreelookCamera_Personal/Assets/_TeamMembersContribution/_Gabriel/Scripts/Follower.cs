using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target; // The object to follow
    public AnimationCurve followCurve = AnimationCurve.Linear(0, 0, 1, 1); // Adjustable animation curve for smoothing

    public float followSpeed = 5f; // Speed of following

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the normalized distance between the follower and the target
            float distanceNormalized = Mathf.Clamp01(Vector3.Distance(transform.position, target.position) / followSpeed);

            // Evaluate the animation curve to get the interpolation factor
            float curveValue = followCurve.Evaluate(distanceNormalized);

            // Smoothly move towards the target
            transform.position = Vector3.Lerp(transform.position, target.position, curveValue * Time.deltaTime * followSpeed);
        }
    }
}
