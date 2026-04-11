using UnityEngine;

[CreateAssetMenu(fileName = "CollectItemQuestObjective", menuName = "Scriptable Objects/QuestObjective/CollectItemQuestObjective")]
public class CollectItemQuestObjectiveData: QuestObjectiveData
{
    public InventoryItemData ItemData;
    public int RequiredAmount;
}
