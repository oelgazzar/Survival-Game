using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject _dialogUI;
    [SerializeField] Image _characterPortraitContainer;
    [SerializeField] TMP_Text _characterNameText;
    [SerializeField] TMP_Text _dialogText;
    [SerializeField] Transform _dialogChoicesContainer;
    [SerializeField] Button _dialogChoiceButtonPrefab;
    [SerializeField] DialogData _testDialogData;
    [SerializeField] Button _closeDialogButton;

    public static DialogManager Instance { get; private set; }

    readonly Dictionary<string, DialogNode> _dialogNodeMap = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _closeDialogButton.onClick.AddListener(EndDialog);

    }

    private void EndDialog()
    {
        _dialogUI.SetActive(false);
        GameManager.Instance.Pause(false);
    }

    public void StartDialog(DialogData dialogData)
    {
        foreach (var node in dialogData.Nodes)
        {
            _dialogNodeMap[node.ID] = node;
        }

        GameManager.Instance.Pause(true, 1);

        _dialogUI.SetActive(true);
        _characterNameText.text = dialogData.CharacterName;
        _characterPortraitContainer.sprite = dialogData.CharacterPortrait;
        
        var startNode = _dialogNodeMap["start"];
        DisplayDialogNode(startNode);
    }

    private void DisplayDialogNode(DialogNode startNode)
    {
        _dialogText.text = startNode.Text;

        ClearOldChoices();

        foreach (var choice in startNode.Choices)
        {
            var choiceButton = Instantiate(_dialogChoiceButtonPrefab, _dialogChoicesContainer);
            choiceButton.GetComponentInChildren<TMP_Text>().text = choice.ChoiceText;
            var nextNode = _dialogNodeMap[choice.NextNodeID];
            choiceButton.onClick.AddListener(() => DisplayDialogNode(nextNode));
        }
    }

    private void ClearOldChoices()
    {
        foreach (Transform child in _dialogChoicesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    [ContextMenu("Test Dialog")]
    void TestDialog()
    {
        StartDialog(_testDialogData);
    }
}
