using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BettingUI : MonoBehaviour
{
    public SlotMachine slotMachine;// Reference to the SlotMachine script
    public GameObject bettingPanel;// Reference to the betting panel UI
    public GameObject slotMachineGameObject;// Reference to the slot machine GameObject
    public TextMeshProUGUI messageText;// Reference to the message text UI
    public TextMeshProUGUI totalPrizeText;//text component to display the total prize
    public Button bet10;// Reference to the 10 bet button
    public Button bet50;// Reference to the 50 bet button
    public Button bet100;// Reference to the 100 bet button
    public Button exit;// Reference to the exit button
    public Button retryButton; // Reference to Retry button
    public AudioSource backgroundMusic; // Reference to the background music AudioSource
    private bool firstSpinDone = false;//boolean to check if first spin is done or not
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //applying addlisteners
        bet10.onClick.AddListener(() => PlaceBet(10));
        bet50.onClick.AddListener(() => PlaceBet(50));
        bet100.onClick.AddListener(() => PlaceBet(100));
        exit.onClick.AddListener(ExitGame);
        retryButton.onClick.AddListener(RetryGame);
        bettingPanel.SetActive(false);//hide the betting panel initially
        messageText.gameObject.SetActive(false); // Hide message at start
        retryButton.gameObject.SetActive(false); // Hide retry button at start
    }
    private void ExitGame()
    {
        bettingPanel.SetActive(false);
        slotMachineGameObject.SetActive(false);// Disable slot machine GameObject
        // Stop play mode in editor for testing
        // UnityEditor.EditorApplication.isPlaying = false;
        messageText.gameObject.SetActive(true);
        messageText.text = "Game Exited";
        totalPrizeText.gameObject.SetActive(false); // Hide total prize text

        // Quit in standalone build
        Application.Quit();

        // For testing in editor
        Debug.Log("Exit Game");
        return;
    }
    public void OpenBettingPanel()
    {
        bettingPanel.SetActive(true);//show the betting panel
    }
    private void PlaceBet(int amount)
    {
        if (!firstSpinDone)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "First spin must be done manually!";
            return;
        }
        if (slotMachine.GetTotalPrizeValue() < amount)
        {
            // Show insufficient funds message and block spin
            messageText.gameObject.SetActive(true);
            messageText.text = "Not enough funds! Bad luck!";
            bettingPanel.SetActive(false); // Hide betting panel
            slotMachineGameObject.SetActive(false);// Disable slot machine GameObject
            totalPrizeText.gameObject.SetActive(false); // Hide total prize text
            retryButton.gameObject.SetActive(true);// Show retry button
            return; //No spin happens
        }
        slotMachine.AdjustTotalPrize(-amount);//deduct Bet
        bettingPanel.SetActive(false);//hide the betting panel
        messageText.gameObject.SetActive(false); // Hide message after placing bet
        slotMachine.StartSpinFromBet();//start spinning the reels
    }

    // Method to mark the first spin as done
    public void MarkFirstSpinDone()
    {
        firstSpinDone = true;//first spin is set to true
    }

    private void RetryGame()
    {
        slotMachine.ResetSlotMachine();//Reset the slot machine

        // Reset Betting UI
        firstSpinDone = false;
        messageText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        totalPrizeText.gameObject.SetActive(true);
        slotMachineGameObject.SetActive(true);
        bettingPanel.SetActive(false);

        //Reseting the music
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
            backgroundMusic.Play();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
