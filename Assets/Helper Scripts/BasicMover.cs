using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    [SerializeField] private Vector3 PointA;
    [SerializeField] private Vector3 PointB;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float StartLerp = 0f;

    private bool movingForward = true;
    private Vector3 nextPoint;

    public void Initialise(Vector3 pointA, Vector3 pointB, float speed = 1f, float startLerp = 0f)
    {
        PointA = pointA;
        PointB = pointB;
        Speed = speed;
        StartLerp = startLerp;
        BeginMove();
    }

    private void Start()
    {
        BeginMove();
    }
    void Update()
    {
        Vector3 direction = nextPoint - transform.position;

        transform.position += direction.normalized * Time.deltaTime * Speed;

        if (Vector3.Distance(transform.position, nextPoint) < 0.1f)
        {
            nextPoint = movingForward ? PointA : PointB;
            movingForward = !movingForward;
        }
    }

    private void BeginMove()
    {
        Vector3 diff = PointB - PointA;
        transform.position = PointA + (diff * StartLerp);
        nextPoint = PointB;
    }
}
