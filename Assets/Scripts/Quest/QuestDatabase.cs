using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "Scriptable Objects/QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
    [SerializeField] QuestData[] _quests;

    public Dictionary<string, QuestData> QuestLookUp { get; private set; }

    private void OnEnable()
    {
        QuestLookUp = new Dictionary<string, QuestData>();

        foreach (var quest in _quests)
        {
            QuestLookUp.Add(quest.ID, quest);
        }
    }
}
