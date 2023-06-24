using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private LayerMask ObstacleLayerMask;
    [SerializeField] private LayerMask CompletionGemLayerMask;
    private bool _hasCollidedThisFrame = false;

    private void Update()
    {
        _hasCollidedThisFrame = false;
    }

    private async void OnTriggerEnter(Collider collision)
    {
        if (_hasCollidedThisFrame) { return; }
        if (1 << collision.gameObject.layer == ObstacleLayerMask)
        {
            _hasCollidedThisFrame = true;
            GetComponent<PlayerController>().Kill();
            await Wait();
            StageManager.Instance.RestartStage();
        }
        else if (1 << collision.gameObject.layer == CompletionGemLayerMask)
        {
            _hasCollidedThisFrame = true;
            StageManager.Instance.CompleteStage();
        }
    }

    private async Task Wait()
    {
        await Task.Delay(500);
    }

}
