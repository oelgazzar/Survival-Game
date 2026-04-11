using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterPortrait;
    public TextAsset DialogTextAsset;
}
