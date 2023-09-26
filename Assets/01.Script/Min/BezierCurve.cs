using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BezierCurve : MonoBehaviour
{
    public Vector2 startVec;
    public Vector2 middleVec;
    public Vector2 endVec;

    public Transform startPoint;
        public Transform middlePoint, endPoint;
    public LineRenderer lineRenderer;



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
    }
    public void SetTrms(Vector2 start, Vector2 middle, Vector2 end)
    {
        startVec = start;
        middleVec = middle;
        endVec = end;
    }

    public void GetMiddlePoint(Vector2 start, Vector2 end)
    {
        Vector2 valueVec;

        Vector2 vec = new Vector2(start.x / end.x, start.y / end.y);
        valueVec = vec;
        middleVec = valueVec;
       SetTrms(start, middleVec, end);
    }

    public void DrawBezierCurve(int index)
    {
        //lineRenderer.positionCount = 20; // 선의 해상도를 설정합니다.
        //lineRenderer.startWidth = 0.f;
        //lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount += 3; 

        lineRenderer.SetPosition(index, startVec);
        lineRenderer.SetPosition(index+1, endVec);
        lineRenderer.SetPosition(index + 2, startVec);


        //for (int i = 0; i < lineRenderer.positionCount; i++)
        //{
        //    float t = i / (float)(lineRenderer.positionCount - 1);
        //    Vector3 pointOnCurve = CalculateBezierPoint(t, startVec, middleVec, endVec);

        //    lineRenderer.SetPosition(i, pointOnCurve);
        //    Debug.Log("Sdds");
        //}
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
