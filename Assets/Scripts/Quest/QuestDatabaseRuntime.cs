using UnityEngine;

public class QuestDatabaseRuntime : MonoBehaviour
{
    [SerializeField] QuestDatabase _questDatabase;

    public static QuestDatabaseRuntime Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public QuestData GetQuest(string id)
    {
        return _questDatabase.QuestLookUp[id];
    }
}
