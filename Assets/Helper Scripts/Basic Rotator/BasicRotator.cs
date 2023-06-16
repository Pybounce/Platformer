using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BasicRotator : MonoBehaviour
{

    [SerializeField] private BasicRotatorInput Input;


    private Vector3 _startRotation;
    private Vector3 _rotation;
    /// <summary>
    /// Stops Start from overriding init input.
    ///<br>If you initialise the object right after instantiating through code, start will be called on the next frame. So this stops it from overriding the input.</br>
    /// </summary>
    private bool _manuallyInitialised;

    public void Initialise(BasicRotatorInput input, bool manualInitialisation = true)
    {
        _startRotation = input.StartRotation;
        _rotation = input.Rotation;
        _manuallyInitialised = manualInitialisation || _manuallyInitialised;
        BeginRotator();
    }
    private void Start()
    {
        if (_manuallyInitialised == false)
        {
            Initialise(Input, false);
        }
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

