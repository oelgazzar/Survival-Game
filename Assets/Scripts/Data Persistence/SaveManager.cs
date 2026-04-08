using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
public class SaveManager
{
    public static void Save()
    {
        var data = new List<SaveableEntity>();
        
        var saveables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
        foreach (var saveable in saveables)
        {
            var id = saveable.SaveID;
            var state = saveable.Save();
            
            data.Add(new SaveableEntity()
            {
                ID = id,
                State = state
            });
        }

        var json = JsonUtility.ToJson(new SerializationWrapper<SaveableEntity>(data));
        var path = Application.persistentDataPath + $"/save_slot_{GameSession.SaveSlot}.json";

        File.WriteAllText(path, json);
    }

    public static void Load(int saveSlot)
    {
        var path = Application.persistentDataPath + $"/save_slot_{saveSlot}.json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found at " + path);
            return;
        }
        var json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<SerializationWrapper<SaveableEntity>>(json).Data;
        var saveables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
        foreach (var saveable in saveables)
        {
            var id = saveable.SaveID;
            var state = data.FirstOrDefault(x => x.ID == id)?.State;
            if (state != null)
            {
                saveable.Load(state);
            }
        }
    }
}
