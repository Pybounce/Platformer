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
    private AnimationCurve _speedOverTime = new AnimationCurve();

    /// <summary>
    /// Stops Start from overriding init input.
    ///<br>If you initialise the object right after instantiating through code, start will be called on the next frame. So this stops it from overriding the input.</br>
    /// </summary>
    private bool _manuallyInitialised;

    public void Initialise(BasicMoverInput input, bool manualInitialisation = true)
    {
        _offset = input.Offset;
        _speed = input.Speed;
        _startLerp = input.StartLerp;
        _manuallyInitialised = manualInitialisation || _manuallyInitialised;
        SetupSpeedOverTime();
        BeginMove();

    }

    private void Start()
    {
        if (_manuallyInitialised == false)
        {
            Initialise(Input, false);
        }
    }
    void Update()
    {
        
        Vector3 direction = _nextPoint - _currentPoint;
        float distanceToTarget = (_nextPoint - transform.position).magnitude;
        float distanceBetweenTargets = (_nextPoint - _currentPoint).magnitude;

        float currentSpeed = Mathf.Min(_speedOverTime.Evaluate(distanceToTarget / distanceBetweenTargets) *_speed, _speed);

        Vector3 step = direction.normalized * Time.deltaTime * currentSpeed;
        float stepDistance = (step).magnitude;
        transform.position += step;

        if (stepDistance >= distanceToTarget)
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

    private void SetupSpeedOverTime()
    {
        _speedOverTime.AddKey(0f, 0.1f);
        _speedOverTime.AddKey(0.05f, 1f);
        _speedOverTime.AddKey(0.95f, 1f);
        _speedOverTime.AddKey(1f, 0.1f);
    }
}
