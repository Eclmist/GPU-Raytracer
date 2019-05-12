using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RaytraceMain : MonoBehaviour
{
    [SerializeField] private ComputeShader m_RaytraceCS;

    private const int threadGroupSize = 8;      // If you change this you have to change Raytrace.compute as well
    private RenderTexture m_TargetRT;
    private Camera m_Camera;
}
