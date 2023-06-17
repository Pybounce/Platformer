using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// How violent the shake effect is.
    /// </summary>
    [SerializeField] private float ShakeMagnitude = 0.3f;
    /// <summary>
    /// How long the shake effect lasts
    /// </summary>
    [SerializeField] private float ShakeTime = 0.2f;

    private Transform _playerTransform;
    private float _shakeTimeRemaining = 0f;
    /// <summary>
    /// The position of the camera without any shake offsets.
    /// </summary>
    private Vector3 _realPosition;
    private bool _isShaking => _shakeTimeRemaining > 0f;
    public void TriggerShake()
    {
        _shakeTimeRemaining = ShakeTime;
    }

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(0f, 0f, -50);
        _realPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        direction.z = 0f;
        _realPosition += direction * Time.deltaTime * 250f;
        transform.position = _realPosition;
        HandleCameraShake();

    }
    private void HandleCameraShake()
    {
        if (_isShaking == false) { return; }
        Vector3 shakeOffset = Random.insideUnitSphere * ShakeMagnitude;
        _shakeTimeRemaining -= Time.deltaTime;
        shakeOffset.z = 0f;
        transform.position += shakeOffset;

    }
}
