using UnityEngine;
using Cinemachine;

public class SmoothFreeLookCameraBehavior : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;

    public float topRigDamping = 5f;
    public float middleRigDamping = 5f;
    public float bottomRigDamping = 5f;

    void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        // Ensure the free look camera is not null
        if (freeLookCamera == null)
        {
            Debug.LogError("Cinemachine FreeLook Camera component not found.");
            return;
        }

        // Set damping for TopRig
        CinemachineOrbitalTransposer topRigTransposer = freeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (topRigTransposer != null)
        {
            topRigTransposer.m_XDamping = topRigDamping;
        }

        // Set damping for MiddleRig
        CinemachineOrbitalTransposer middleRigTransposer = freeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (middleRigTransposer != null)
        {
            middleRigTransposer.m_XDamping = middleRigDamping;
        }

        // Set damping for BottomRig
        CinemachineOrbitalTransposer bottomRigTransposer = freeLookCamera.GetRig(2).GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (bottomRigTransposer != null)
        {
            bottomRigTransposer.m_XDamping = bottomRigDamping;
        }
    }
}