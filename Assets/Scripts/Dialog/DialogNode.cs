using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogNode
{
    public string ID;
    public string Text;
    public List<DialogChoice> Choices;
    public string Action; // Optional action to trigger when this node is reached
}
