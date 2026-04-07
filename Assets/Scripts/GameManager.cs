using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public enum GameState
    {
        Playing, Paused
    }

    public GameState State;

    private void Start()
    {
        State = GameState.Playing;
    }

    public void Pause(bool value)
    {
        State = value? GameState.Paused : GameState.Playing;
        Time.timeScale = value ? 0 : 1; 
    }
}
