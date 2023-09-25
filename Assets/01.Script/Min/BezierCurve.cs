using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform startPoint, middlePoint, endPoint;
    public LineRenderer lineRenderer;



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
    }

    void DrawBezierCurve()
    {
        lineRenderer.positionCount = 20; // 선의 해상도를 설정합니다.

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float t = i / (float)(lineRenderer.positionCount - 1);
            Vector3 pointOnCurve = CalculateBezierPoint(t, startPoint.position, middlePoint.position, endPoint.position);

            lineRenderer.SetPosition(i, pointOnCurve);
        }
    }
        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // term (1-t)^2 * P0
        p += 2 * u * t * p1; // term 2*(1-t)*t * P1
        p += tt * p2; // term t^2 * P2

        return p;
    }
}
