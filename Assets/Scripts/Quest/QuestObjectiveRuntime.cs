using System;
using UnityEngine;

public abstract class QuestObjectiveRuntime
{
    public event Action<QuestObjectiveRuntime> ObjectiveCompleted;
    public bool IsCompleted { get; protected set; }

    protected void Complete()
    {
        if (IsCompleted) return;
        IsCompleted = true;
        ObjectiveCompleted?.Invoke(this);
    }
    public abstract void Evaluate();
}

public abstract class QuestObjectiveRuntime<T>: QuestObjectiveRuntime where T : QuestObjectiveData
{
    public T Data;

    public QuestObjectiveRuntime(T data)
    {
        Data = data;
    }

    public static QuestObjectiveRuntime CreateRuntimeObjective(T data)
    {
        return data switch
        {
            CollectItemQuestObjectiveData collect =>
                new CollectItemQuestObjectiveRuntime(collect),

            //TalkToNpcQuestObjective talk =>
            //    new TalkToNpcObjectiveRuntime(talk),

            //ExploreAreaQuestObjective explore =>
            //    new ExploreAreaObjectiveRuntime(explore),

            //BuildStructureQuestObjective build =>
            //    new BuildStructureObjectiveRuntime(build),

            _ => null
        };
    }
}
