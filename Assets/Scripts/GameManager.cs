using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (GameSession.IsLoadingGame)
        {
            LoadGame();
            GameSession.IsLoadingGame = false;
        }
    }

    public void Pause(bool value, float timeScaleOnPause = 0)
    {
        State = value? GameState.Paused : GameState.Playing;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = value ? timeScaleOnPause : 1; 
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        Debug.Log("Game saved!");
        SaveManager.Save();
    }

    public void LoadGame()
    {
        Debug.Log("Game loaded!");
        SaveManager.Load(GameSession.SaveSlot);
    }
}
