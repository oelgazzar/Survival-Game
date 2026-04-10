using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterPortrait;
    public List<DialogNode> Nodes;
}
