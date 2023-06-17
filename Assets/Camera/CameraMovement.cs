using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform _playerTransform;
    private Vector3 _shakeOffset = new Vector3(0f, 0f, 0f);  
    private float _shakeTimeRemaining = 0f;

    public void TriggerShake()
    {
        _shakeTimeRemaining = 0.2f;
    }

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _shakeOffset = Random.insideUnitSphere * 0.3f;
        if (_shakeTimeRemaining <= 0f) { _shakeOffset = Vector3.zero; }
        _shakeTimeRemaining -= Time.deltaTime;
        transform.position = _playerTransform.position + new Vector3(0f, 0f, -50) + _shakeOffset;
    }
}
