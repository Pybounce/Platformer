using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float StartLerp = 0f;

    private Vector3 _nextPoint;
    private Vector3 _currentPoint;

    public void Initialise(Vector3 offset, float speed = 1f, float startLerp = 0f)
    {
        Offset = offset;
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
        Vector3 direction = _nextPoint - transform.position;

        transform.position += direction.normalized * Time.deltaTime * Speed;

        if (Vector3.Distance(transform.position, _nextPoint) < 0.1f)
        {
            Vector3 tempCurrentPoint = _currentPoint;
            _currentPoint = _nextPoint;
            _nextPoint = tempCurrentPoint;
        }
    }

    private void BeginMove()
    {
        _currentPoint = transform.position;
        _nextPoint = _currentPoint + Offset;

        Vector3 diff = _nextPoint - _currentPoint;
        transform.position = _currentPoint + (diff * StartLerp);
    }
}
