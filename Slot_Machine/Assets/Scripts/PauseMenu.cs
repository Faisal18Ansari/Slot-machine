using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI
    private bool isPaused = false; // Boolean to track pause state
    public SlotMachine slotMachine; // Reference to the slot machine script
    public BettingUI bettingUI; // Reference to the betting UI script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu at start
        Time.timeScale = 1f; // Ensure game is running at normal speed
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if Escape key is pressed
        {
            if (isPaused)
            {
                ResumeGame(); // Resume the game if currently paused
            }
            else
            {
                PauseGame(); // Pause the game if currently running
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = false; // stop blocking raycasts when hidden
        Time.timeScale = 1f; // Resume game time
        isPaused = false; // Update pause state

        slotMachine.enabled = true;
        bettingUI.enabled = true;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show pause menu
        pauseMenuUI.GetComponent<CanvasGroup>().blocksRaycasts = true; //block clicks when menu is active
        Time.timeScale = 0f; // Stop game time
        isPaused = true; // Update pause state

        slotMachine.enabled = false;
        bettingUI.enabled = false;
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
}
