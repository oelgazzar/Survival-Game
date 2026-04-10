using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DialogueParser
{
    [MenuItem("Tools/Parse Dialogue File")]
    public static void Parse()
    {
        string dialogTextPath = EditorUtility.OpenFilePanel(
            "Select dialogue file",
            "",
            "txt"
        );

        string dialogPortraitPath = EditorUtility.OpenFilePanel(
            "Select character portrait",
            "",
            "png,jpg"
        );

        if (string.IsNullOrEmpty(dialogTextPath))
            return;

        string[] lines = File.ReadAllLines(dialogTextPath);

        DialogData dialog =
            ScriptableObject.CreateInstance<DialogData>();

        dialog.CharacterPortrait =
                    AssetDatabase.LoadAssetAtPath<Sprite>(
                        dialogPortraitPath[dialogPortraitPath.IndexOf("Assets/")..]
                    );

        dialog.Nodes = new List<DialogNode>();

        DialogNode currentNode = null;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line.StartsWith("NPC:"))
            {
                dialog.CharacterName =
                    line.Replace("NPC:", "").Trim();
            }

            else if (line.StartsWith("["))
            {
                currentNode = new()
                {
                    ID =
                        line.Replace("[", "")
                            .Replace("]", ""),

                    Choices =
                        new List<DialogChoice>()
                };

                dialog.Nodes.Add(currentNode);
            }

            else if (line.StartsWith("->"))
            {
                string[] parts =
                    line.Replace("->", "")
                        .Split(':');

                DialogChoice choice =
                    new()
                    {
                        NextNodeID =
                        parts[0].Trim(),

                        ChoiceText =
                        parts[1].Trim()
                    };

                currentNode.Choices.Add(choice);
            }

            else if (line.StartsWith("ACTION:"))
            {
                currentNode.Action =
                    line.Replace("ACTION:", "").Trim();
            }

            else if (!string.IsNullOrEmpty(line))
            {
                currentNode.Text = line;
            }
        }

        AssetDatabase.CreateAsset(
            dialog,
            $"Assets/{dialog.CharacterName}Dialog.asset"
        );

        AssetDatabase.SaveAssets();

        Debug.Log("Dialogue parsed successfully");
    }
}