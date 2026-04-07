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
    }

    public void Pause(bool value)
    {
        State = value? GameState.Paused : GameState.Playing;
        Time.timeScale = value ? 0 : 1; 
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        // Implement save game logic here
        // This could involve saving player stats, inventory, and current level to a file or PlayerPrefs
        Debug.Log("Game saved!");
        // Example: PlayerPrefs.SetInt("PlayerLevel", currentLevel);
        // Example: PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        // Remember to call PlayerPrefs.Save() if you want to ensure the data is written to disk immediately
        PlayerPrefs.Save();
    }
}
