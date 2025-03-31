using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpdateAngleInfo(Transform cameraTransform);
public class CameraController : MonoBehaviour
{
    public event UpdateAngleInfo UpdateAngle;
    private void LateUpdate()
    {
        EulerAnglesClamper.Instance.ClampObjectAngle(transform);
        UpdateAngle?.Invoke(transform);
    }

}
