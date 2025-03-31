using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICameraTransformDependant : MonoBehaviour
{
    protected CameraController cameraController;

    protected virtual void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void OnEnable()
    {
        cameraController.UpdateAngle += Work;
    }
    private void OnDisable()
    {
        cameraController.UpdateAngle -= Work;
    }
    public abstract void Work(Transform cameraTransform);
}
