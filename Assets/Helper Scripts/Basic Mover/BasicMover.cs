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
    [SerializeField] private AnimationCurve _distanceToTargetNormalised = new AnimationCurve();
    private float _timeElapsed = 0f;
    private float _totalTimeForLeg = 0f;

    private float _stoppingDistance = 1f;
    private float _stoppingTime = 0.4f;

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
        _currentPoint = transform.position;
        _nextPoint = _currentPoint + _offset;


        float distanceBetweenTargets = _offset.magnitude;
        _stoppingDistance = Mathf.Min(_stoppingDistance, distanceBetweenTargets * 0.2f);
        _totalTimeForLeg = (distanceBetweenTargets - _stoppingDistance - _stoppingDistance) / _speed;
        _totalTimeForLeg += _stoppingTime * 2f;
        SetupSpeedOverTime();
        SetupStartLerp();
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
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _totalTimeForLeg)
        {
            _timeElapsed -= _totalTimeForLeg;
            Vector3 tempCurrentPoint = _currentPoint;
            _currentPoint = _nextPoint;
            _nextPoint = tempCurrentPoint;
        }

        //print(_timeElapsed);
        transform.position = Vector3.Lerp(_currentPoint, _nextPoint, _distanceToTargetNormalised.Evaluate(_timeElapsed));

    }

    private void SetupStartLerp()
    {
        _timeElapsed = Mathf.Lerp(0f, _totalTimeForLeg, _startLerp);
    }

    private void SetupSpeedOverTime()
    {
        float distanceBetweenTargets = _offset.magnitude;


        Vector2 keyFrameOnePos = new Vector2(_stoppingTime, _stoppingDistance / distanceBetweenTargets);
        Vector2 keyFrameTwoPos = new Vector2(_totalTimeForLeg - _stoppingTime, (distanceBetweenTargets - _stoppingDistance) / distanceBetweenTargets);
        float gradient = (keyFrameTwoPos.y - keyFrameOnePos.y) / (keyFrameTwoPos.x - keyFrameOnePos.x);
        Keyframe[] keyFrames = new Keyframe[4];
        keyFrames[0] = new Keyframe(0f, 0f);
        keyFrames[1] = new Keyframe(keyFrameOnePos.x, keyFrameOnePos.y, gradient, gradient);
        keyFrames[2] = new Keyframe(keyFrameTwoPos.x, keyFrameTwoPos.y, gradient, gradient);
        keyFrames[3] = new Keyframe(_totalTimeForLeg, 1f);
        keyFrames[0].outTangent = 0f;
        keyFrames[3].inTangent = 0f;
        _distanceToTargetNormalised = new AnimationCurve(keyFrames);


    }
}
