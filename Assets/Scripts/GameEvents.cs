using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }
    public event Action<string> OnDialogEventRaised;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void RaiseDialogEvent(string eventName)
    {
        OnDialogEventRaised?.Invoke(eventName);
    }
}
