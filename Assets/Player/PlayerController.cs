using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Material BackWallMat;

    public void ResetTo(Vector3 position)
    {
        transform.position = position;
        GetComponent<BetterPlayerMovementController>().enabled = true;
        GetComponent<BetterPlayerMovementController>().ResetMovement();
        GetComponent<PlayerCollisionController>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<TrailRenderer>().Clear();
    }
    public void Kill()
    {
        GetComponent<BetterPlayerMovementController>().enabled = false;
        GetComponent<PlayerCollisionController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        BackWallMat.SetFloat("_LastHighlightTime", Time.timeSinceLevelLoad);
        GetComponent<ParticleSystem>().Play();
        Camera.main.GetComponent<CameraMovement>().TriggerShake();
    }
}
