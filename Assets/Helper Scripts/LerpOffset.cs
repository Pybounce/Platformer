using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpOffset : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float Speed = 1f;

    private Vector3 PointA;
    private Vector3 PointB;
    private bool movingForward = true;
    private Vector3 nextPoint;
    private Vector3 lastPoint;

    private void Start()
    {
        PointA = transform.position;
        PointB = transform.position + Offset;
        nextPoint = PointB;
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
}
