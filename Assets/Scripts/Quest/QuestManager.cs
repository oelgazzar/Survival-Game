using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    readonly Dictionary<string, QuestRuntime> _quests = new ();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void HandleDialogEvent(string eventName)
    {
        var parts = eventName.Split('_');
        var action = parts[0].Trim();
        var questID = parts[1].Trim();

        if (!_quests.ContainsKey(questID))
        {
            _quests[questID] = new QuestRuntime(QuestDatabaseRuntime.Instance.GetQuest(questID));
        }

        var quest = _quests[questID];

        if (action == "startQuest")
        {
            quest.State = QuestState.InProgress;
        } else if (action == "declineQuest")
        {
            quest.State = QuestState.Declined;
        }
    }

    public QuestState GetQuestState(string questID)
    {
        if (_quests.TryGetValue(questID, out var quest) == false)
        {
            return QuestState.NotStarted;
        }

        return quest.State;
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnDialogEventRaised += HandleDialogEvent;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnDialogEventRaised -= HandleDialogEvent;
    }
}