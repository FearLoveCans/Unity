using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Arrow : MonoBehaviour
{
    public Vector3 startPoint;    // 起始点
    public Vector3 controlPoint;  // 控制点
    public Vector3 endPoint;      // 终点
    public int numPoints = 50;      // 曲线上的点数
    public bool showCurve = false;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        UpdateBezierCurve();


    }

    void Update()
    {
        if (showCurve)
        {
            UpdateBezierCurve();
        }
        else
        {
            lineRenderer.positionCount = 0; // 不显示曲线时，将曲线点数设为0
        }
    }

    void UpdateBezierCurve()
    {
        lineRenderer.positionCount = numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateBezierPoint(t, startPoint, controlPoint, endPoint);
            lineRenderer.SetPosition(i, point);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 point = uu * p0 + 2 * u * t * p1 + tt * p2;
        return point;
    }
}