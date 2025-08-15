using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()// Start the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);// Load the next scene
    }
    public void QuitGame()
    {
        // Stop play mode in editor for testing
        // UnityEditor.EditorApplication.isPlaying = false;

        // Quit in standalone build
        Application.Quit();

        // For testing in editor
        Debug.Log("Game Exited");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
