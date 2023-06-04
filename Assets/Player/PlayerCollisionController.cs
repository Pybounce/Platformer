using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private LayerMask ObstacleLayerMask;
    [SerializeField] private LayerMask CompletionGemLayerMask;
    private StageManager _stageManager;
    private void Start()
    {
        _stageManager = FindObjectOfType<StageManager>();
    }

    private async void OnTriggerEnter(Collider collision)
    {
        if (1 << collision.gameObject.layer == ObstacleLayerMask)
        {
            GetComponent<PlayerController>().Kill();
            await Wait();
            _stageManager.RestartStage();
        }
        else if (1 << collision.gameObject.layer == CompletionGemLayerMask)
        {
            _stageManager.CompleteStage();
        }
    }

    private async Task Wait()
    {
        await Task.Delay(500);
    }

}
