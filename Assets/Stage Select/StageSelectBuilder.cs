using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectBuilder : MonoBehaviour
{
    [SerializeField] private GameObject CanvasObject;
    [SerializeField] private GameObject StageSelectButton;
    [SerializeField] private float ButtonSpacing;
    [SerializeField] private int RowCount = 5;
    private float _buttonWidth;
    private float _buttonHeight;

    [SerializeField] private GameObject StageCompletionItem;
    [SerializeField] private GameObject DevTimeCompletionItem;
    [SerializeField] private GameObject MirrorModeCompletionItem;
    [SerializeField] private GameObject BlankCompletionItem;
    private void Start()
    {
        _buttonWidth = StageSelectButton.GetComponent<RectTransform>().rect.width;
        _buttonHeight = StageSelectButton.GetComponent<RectTransform>().rect.height;
        GameData gameData = GameDb.LoadGame();
        for (int i = 0; i < gameData.StagesData.Count; i++)
        {
            PlayerStageData playerStageData = gameData.StagesData[i];
            GameObject newButton = Instantiate(StageSelectButton);
            newButton.transform.SetParent(CanvasObject.transform);
            newButton.transform.localPosition = new Vector3((_buttonWidth + ButtonSpacing) * i, (_buttonHeight + ButtonSpacing) * (i / RowCount), 1f);
            newButton.transform.localScale = Vector3.one;

            EventTrigger trigger = newButton.gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener((eventData) => { FindObjectOfType<StageSelectManager>().LoadStage(playerStageData.StageId); });
            trigger.triggers.Add(entry);

            AddCompletionItem(StageCompletionItem, 0, newButton.transform, playerStageData.StageComplete);
            AddCompletionItem(DevTimeCompletionItem, 1, newButton.transform, playerStageData.DevTimeComplete);
            AddCompletionItem(MirrorModeCompletionItem, 2, newButton.transform, playerStageData.MirrorModeComplete);
        }
    }
    private void AddCompletionItem(GameObject completionItem, int position, Transform parent, bool condition)
    {
        GameObject newCompletionItem;
        if (condition)
        {
            newCompletionItem = Instantiate(completionItem);
        }
        else
        {
            newCompletionItem = Instantiate(BlankCompletionItem);
        }
        newCompletionItem.transform.parent = parent;
        Vector3 positionOffset = new Vector3(-_buttonWidth / 3f, -_buttonHeight / 3f, 0f);
        newCompletionItem.transform.localPosition = new Vector3(position * (_buttonWidth / 3f), 0f, 0f) + positionOffset;

    }

}
