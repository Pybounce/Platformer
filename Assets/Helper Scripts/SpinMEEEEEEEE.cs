using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SpinMEEEEEEEE : MonoBehaviour
{
    [SerializeField] private Vector3 Rotation;
    [SerializeField] private float Speed = 1f;

    void Update()
    {
        transform.Rotate(Rotation  * Speed * Time.deltaTime);
    }
}
