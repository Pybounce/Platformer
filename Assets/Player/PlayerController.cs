using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void ResetTo(Vector3 position)
    {
        transform.SetParent(null);
        transform.position = position;
        GetComponent<BetterPlayerMovementController>().enabled = true;
        GetComponent<BetterPlayerMovementController>().ResetMovement();
        GetComponent<PlayerCollisionController>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<TrailRenderer>().Clear();
    }
    public void Kill()
    {
        transform.SetParent(null);
        GetComponent<BetterPlayerMovementController>().enabled = false;
        GetComponent<PlayerCollisionController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        Camera.main.GetComponent<CameraMovement>().TriggerShake();
    }
}
