using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] GameObject _dialogUI;
    [SerializeField] Image _characterPortraitContainer;
    [SerializeField] TMP_Text _characterNameText;
    [SerializeField] TMP_Text _dialogText;
    [SerializeField] Transform _dialogChoicesContainer;
    [SerializeField] Button _dialogChoiceButtonPrefab;
    [SerializeField] Button _closeDialogButton;

    public static DialogManager Instance { get; private set; }

    readonly Dictionary<string, DialogState> _dialogStateMap = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _closeDialogButton.onClick.AddListener(EndDialog);
    }

    public void StartDialog(DialogData dialogData)
    {
        if (_dialogStateMap.TryGetValue(dialogData.CharacterName, out var dialogState) == false)
        {
            dialogState = new DialogState
            {
                Story = new Story(dialogData.DialogTextAsset.text),
                IsFirstTime = true
            };
            _dialogStateMap[dialogData.CharacterName] = dialogState;

        }

        var story = dialogState.Story;
        var questName = "";
        foreach (var tag in story.globalTags)
        {
            if (tag.StartsWith("quest:"))
            {
                questName = tag["quest:".Length..].Trim();
                break;
            }
        }
        story.variablesState["questState"] = QuestManager.Instance.GetQuestState(questName).ToString();
        story.variablesState["firstTime"] = dialogState.IsFirstTime;
        dialogState.IsFirstTime = false;
        story.ChoosePathString("entry");
        
        ShowDialogPanel(dialogData);

        ResumeDialog(story);
    }

    public void ShowDialogPanel(DialogData dialogData)
    {
        GameManager.Instance.Pause(true, 1);

        _dialogUI.SetActive(true);
        _characterNameText.text = dialogData.CharacterName;
        _characterPortraitContainer.sprite = dialogData.CharacterPortrait;
    }

    private void ResumeDialog(Story story)
    {
        if (story.canContinue)
        {
            story.Continue();
            _dialogText.text = story.currentText;

            foreach (var tag in story.currentTags)
            {
                if (tag.StartsWith("event:"))
                {
                    var eventName = tag.Split(":")[1].Trim();
                    GameEvents.Instance.RaiseDialogEvent(eventName);
                }
            }

            ClearOldChoiceButtons();
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                var choiceButton = Instantiate(_dialogChoiceButtonPrefab, _dialogChoicesContainer);
                choiceButton.GetComponentInChildren<TMP_Text>().text = story.currentChoices[i].text;
                int choiceIndex = i; // Capture the current index for the lambda
                choiceButton.onClick.AddListener(() => SelectChoice(story, choiceIndex));
            }
        }
        
        if (story.canContinue == false && story.currentChoices.Count == 0)
        {
            EndDialog();
        }
    }

    private void ClearOldChoiceButtons()
    {
        foreach (Transform child in _dialogChoicesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    void SelectChoice(Story story, int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        ResumeDialog(story);
    }

    private void EndDialog()
    {
        _dialogUI.SetActive(false);
        GameManager.Instance.Pause(false);
    }
}
