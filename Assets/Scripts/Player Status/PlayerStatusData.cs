using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "Scriptable Objects/PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Sprite Color;
    public int MaxValue;
}
