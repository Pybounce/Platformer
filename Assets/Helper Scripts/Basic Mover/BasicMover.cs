using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    [SerializeField] private BasicMoverInput Input;


    private Vector3 _offset;
    private float _speed;
    private float _startLerp;
    private Vector3 _nextPoint;
    private Vector3 _currentPoint;

    public void Initialise(BasicMoverInput input)
    {
        _offset = input.Offset;
        _speed = input.Speed;
        _startLerp = input.StartLerp;
        BeginMove();
    }

    private void Start()
    {
        Initialise(Input);
    }
    void Update()
    {
        Vector3 direction = _nextPoint - transform.position;

        transform.position += direction.normalized * Time.deltaTime * _speed;

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
        _nextPoint = _currentPoint + _offset;

        Vector3 diff = _nextPoint - _currentPoint;
        transform.position = _currentPoint + (diff * _startLerp);
    }
}
