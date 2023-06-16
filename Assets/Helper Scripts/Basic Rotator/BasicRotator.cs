using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BasicRotator : MonoBehaviour
{

    [SerializeField] private BasicRotatorInput Input;


    private Vector3 _startRotation;
    private Vector3 _rotation;

    public void Initialise(BasicRotatorInput input)
    {
        _startRotation = input.StartRotation;
        _rotation = input.Rotation;
        BeginRotator();
    }
    private void Start()
    {
        Initialise(Input);
    }
    private void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
    private void BeginRotator()
    {
        transform.rotation = Quaternion.Euler(_startRotation);
    }
}

