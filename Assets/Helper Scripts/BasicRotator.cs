using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BasicRotator : MonoBehaviour
{
    [SerializeField] private Vector3 StartRotation;
    [SerializeField] private Vector3 Rotation;

    public void Initialise(Vector3 rotation, Vector3 startRotation = default)
    {
        StartRotation = startRotation;
        Rotation = rotation;
        BeginRotator();
    }
    private void Start()
    {
        BeginRotator();
    }
    private void Update()
    {
        transform.Rotate(Rotation * Time.deltaTime);
    }
    private void BeginRotator()
    {
        transform.rotation = Quaternion.Euler(StartRotation);
    }
}

