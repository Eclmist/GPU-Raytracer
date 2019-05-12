using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RaytraceMain : MonoBehaviour
{
    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void SetShaderParams()
    {
        // Camera Matrices
        m_RaytraceCS.SetMatrix("_CameraToWorldMat", m_Camera.cameraToWorldMatrix);
        m_RaytraceCS.SetMatrix("_CameraInvProjMat", m_Camera.projectionMatrix.inverse);

        // Common Constant Buffer
        m_RaytraceCS.SetFloat("_Time", Random.value);

        // Textures
        m_RaytraceCS.SetTexture(0, "_SkyboxTexture", RenderSettings.skybox.GetTexture("_MainTex"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        SetShaderParams();

        Render(destination);
    }

    private void InitRenderTarget()
    {
        // If resizing
        if (m_TargetRT != null)
            m_TargetRT.Release();

        m_TargetRT = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
        m_TargetRT.enableRandomWrite = true; // Init as typed UAV
        m_TargetRT.Create();
    }

    private void Render(RenderTexture destination)
    {
        if (m_TargetRT == null || m_TargetRT.width != Screen.width || m_TargetRT.height != Screen.height)
        {
            InitRenderTarget();
        }

        m_RaytraceCS.SetTexture(0, "Target", m_TargetRT);
        int nbThreadGroupX = Mathf.CeilToInt(Screen.width / threadGroupSize);
        int nbThreadGroupY = Mathf.CeilToInt(Screen.height / threadGroupSize);

        m_RaytraceCS.Dispatch(0, nbThreadGroupX, nbThreadGroupY, 1);

        // Blit to screen
        Graphics.Blit(m_TargetRT, destination);
    }
}
