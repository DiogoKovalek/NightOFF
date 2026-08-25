using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class PowerCableBody : MonoBehaviour
{
    [SerializeField] private Transform startP;
    [SerializeField] private Transform endP;
    [SerializeField] private Transform curveP;
    [SerializeField] int resolution = 15;

    private LineRenderer lineRenderer;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update() {
        if(startP == null || endP == null || curveP == null) return;
        if(lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer.positionCount != resolution) lineRenderer.positionCount = resolution;

        DrawCurve();
    }
    
    private void DrawCurve() {
        for(int i = 0; i < resolution; i++) {
            float t = i / (float)(resolution - 1);
            Vector3 position = Bezier(t, startP.position, curveP.position, endP.position);
            lineRenderer.SetPosition(i, position);
        }
    }

    private Vector3 Bezier(float t, Vector3 pStart, Vector3 pMiddle, Vector3 pEnd) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        return (uu * pStart) + (2 * u * t * pMiddle) + (tt * pEnd);
    }
}
