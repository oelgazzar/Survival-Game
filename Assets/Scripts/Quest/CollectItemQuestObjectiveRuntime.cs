using System;
using System.Collections.Generic;

public class CollectItemQuestObjectiveRuntime: QuestObjectiveRuntime<CollectItemQuestObjectiveData>
{
    public CollectItemQuestObjectiveRuntime(CollectItemQuestObjectiveData data) : base(data){
        InventorySystem.InventoryChanged += OnInventoryChanged;
        Evaluate();
    }

    private void OnInventoryChanged(List<InventorySlot> _)
    {
        Evaluate();
    }

    public override void Evaluate()
    {
        var available = InventorySystem.Instance.GetItemCount(Data.ItemData);
        var required = Data.RequiredAmount;
        if (available >= required)
        {
            InventorySystem.InventoryChanged -= OnInventoryChanged;
            Complete();
        }
    }
}
