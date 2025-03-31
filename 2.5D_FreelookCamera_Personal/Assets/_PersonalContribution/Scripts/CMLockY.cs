using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CMLockY : CinemachineExtension
{
    [Tooltip("Lock the camera's Z position to this value")]
    [SerializeField]
    private float m_YPosition = 30;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawOrientation;
            pos.x = m_YPosition;
            state.RawOrientation = pos;
        }
    }
}


