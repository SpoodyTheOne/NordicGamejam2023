using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    public Camera theCamera;

    public List<Transform> targets;

    public Vector3 offset;
    private Vector3 velocity;

    public float smoothTime = 0.5f;
    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter;

    public float actualZoom;

    //public Animator camAnim;

    private float zoomTime;
    public float zoomSpeed;

    public void LateUpdate()
    {
        zoomTime -= Time.deltaTime;

        if (zoomTime <= 0)
        {
            actualZoom = maxZoom;
        }

        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(actualZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, newZoom, Time.deltaTime * zoomSpeed);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    /*public void Shake()
    {
        camAnim.SetTrigger("Shake");
    }*/

    public void EffectZoom(float strength, float time)
    {
        zoomTime = time;
        actualZoom = strength;
    }
}
