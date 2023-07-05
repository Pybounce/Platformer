using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private LayerMask ObstacleLayerMask;
    [SerializeField] private LayerMask CompletionItemLayerMask;
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
            StageManager.Instance.FreezeStage();
            await Wait();
            StageManager.Instance.RestartStage();
        }
        else if (1 << collision.gameObject.layer == CompletionItemLayerMask)
        {
            _hasCollidedThisFrame = true;
            GetComponent<PlayerController>().Disable();
            StageManager.Instance.FreezeStage();
            collision.gameObject.GetComponent<CompletionItemController>().Collect();
            await Wait();
            StageManager.Instance.CompleteStage();
        }
    }

    private async Task Wait(int time = 500)
    {
        await Task.Delay(time);
    }

}
