using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{

    [SerializeField] private Vector3 PointA;
    [SerializeField] private Vector3 PointB;
    [SerializeField] private float Speed = 1f;

    private bool movingForward = true;
    private Vector3 nextPoint;

    private void Start()
    {
        transform.position = PointA;
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
