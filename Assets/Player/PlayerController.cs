
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void ResetTo(Vector3 position)
    {
        transform.position = position;
        GetComponent<BetterPlayerMovementController>().enabled = true;
        GetComponent<BetterPlayerMovementController>().ResetMovement();
        GetComponent<PlayerCollisionController>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<TrailRenderer>().Clear();
        transform.SetParent(null);
    }
    public void Kill()
    {
        Disable();
        GetComponent<ParticleSystem>().Play();
        Camera.main.GetComponent<CameraMovement>().TriggerShake();
    }
    public void Disable()
    {
        transform.SetParent(null);
        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<BetterPlayerMovementController>().enabled = false;
        GetComponent<PlayerCollisionController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

    }
}
