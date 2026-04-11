using System;
using System.Collections.Generic;

public class QuestRuntime
{
    public event Action<QuestRuntime> QuestCompleted;

    public QuestData Data;
    public QuestState State = QuestState.NotStarted;
    public int Progress;
    public int ObjectiveCount => Data.Objectives.Length;
    public List<QuestObjectiveRuntime> Objectives { get; private set; }

    public QuestRuntime(QuestData data)
    {
        Data = data;
        Objectives = new List<QuestObjectiveRuntime>();
        foreach(var objectiveData in Data.Objectives)
        {
            var objective = QuestObjectiveRuntime<QuestObjectiveData>.CreateRuntimeObjective(objectiveData);
            objective.ObjectiveCompleted += OnObjectiveCompleted;
            Objectives.Add(objective);
        }
    }

    private void OnObjectiveCompleted(QuestObjectiveRuntime objective)
    {
        Progress++;
        if (Progress == ObjectiveCount)
        {
            Complete();
        }
    }

    void Complete()
    {
        State = QuestState.Completed;
        QuestCompleted?.Invoke(this);
    }
}
