using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
public class SlotMachine : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };//stating that a global event has been created and calling out other scripts

    [Header("UI Elements")]
    public TextMeshProUGUI prizeText;//text component to display the prize
    public TextMeshProUGUI totalPrizeText;//text component to display the total prize
    public TextMeshProUGUI jackpotText;//text component to display the jackpot

    [Header("References")]
    public Sprite normalSprite;//access to normal sprite
    public Sprite downSprite;//access to down sprite
    private SpriteRenderer spriteRenderer;//sprite renderer for the handle
    public BettingUI bettingUI;// Reference to the BettingUI script
    private AudioSource audioSource;//audio source to play the audio clips

    [Header("Slot Rows")]
    public Row[] rows;//array of rows to hold the symbols

    [Header("Slot Settings")]
    private int prizeValue;//value of the current prize
    private int totalPrizeValue; // Running total prize

    [Header("Game State")]
    private bool resultsChecked = false;//boolean to check if results have been checked
    private bool isFirstSpin = true;//boolean to check if first spin is done

    [Header("Audio Elements")]
    public AudioClip jackpotClip;//audio clip for jackpot
    public AudioClip winClip;//audio clip for win

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//getting the sprite renderer component
        spriteRenderer.sprite = normalSprite;
        totalPrizeValue = 30;
        UpdateTotalPrizeValue();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        jackpotText.gameObject.SetActive(false);//hiding the jackpot text at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            // If any row is still spinning, we can't check the following
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {   //once all the rows stops and if the result is not checked then check the result
            CheckResults();
            resultsChecked = true;
            totalPrizeValue += prizeValue; // Add current prize to total
            UpdateTotalPrizeValue();          // Refresh UI
            prizeText.enabled = true;
            prizeText.text = "PRIZE: " + prizeValue;
        }
        if (!isFirstSpin && rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && resultsChecked)
        {
            bettingUI.OpenBettingPanel();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnMouseDown();// Call OnMouseDown when the mouse button is pressed
        }
    }
    private void OnMouseDown()
    {       // Block manual handle pull if betting UI is active
        if (bettingUI != null && bettingUI.gameObject.activeSelf)
        {
            Debug.Log("Handle disabled because betting UI is active");
            return;
        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine(PullHandle());// Start pulling the handle if all rows are stopped
        }
        if (isFirstSpin)
        {
            isFirstSpin = false;// Mark the first spin as done
            bettingUI.MarkFirstSpinDone();// Notify betting UI that first spin is done
        }
    }
    private IEnumerator PullHandle()
    {
        spriteRenderer.sprite = downSprite;//turn the downSprite on
        HandlePulled();
        yield return new WaitUntil(() =>
            rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped//wait till all the rows are stopped to pull the handle to its normal sprite
        );
        spriteRenderer.sprite = normalSprite;//turn the normalSprite on
    }
    private void CheckResults()
    {
        //Define prize value for 3 matches
        Dictionary<string, int> threeMatchPrizes = new Dictionary<string, int>()
        {
            {"Cherry", 100},
            {"Bell", 80},
            {"Bar", 150},
            {"Seven", 200}
        };
        //Define Prize Value for 2 matches
        Dictionary<string, int> twoMatchPrizes = new Dictionary<string, int>()
        {
            {"Cherry", 50},
            {"Bell", 40},
            {"Bar", 75},
            {"Seven", 100}
        };
        string slot1 = rows[0].stoppedSlot;//creating a slot to check for the necessary conditions
        string slot2 = rows[1].stoppedSlot;
        string slot3 = rows[2].stoppedSlot;
        prizeValue = 0; //Reset before calculating
        //check for 3 matches
        if (slot1 == slot2 && slot2 == slot3)
        {
            if (threeMatchPrizes.ContainsKey(slot1))//if all 3 slots matches then give the prize value
            {
                prizeValue = threeMatchPrizes[slot1];
                StartCoroutine(PlayJackPot());//play jackpot sound
                return;
            }
        }

        //check for 2 matches
        else
        {
            if ((slot1 == slot2 && twoMatchPrizes.ContainsKey(slot1)) ||
            (slot1 == slot3 && twoMatchPrizes.ContainsKey(slot1)) ||
            (slot2 == slot3 && twoMatchPrizes.ContainsKey(slot2)))
            {
                //pick the matched symbol
                string matchedSymbol = slot1 == slot2 ? slot1 : (slot1 == slot3 ? slot1 : slot2);
                prizeValue = twoMatchPrizes[matchedSymbol];//if matched symbol found, assign prize value
                PlayWinSound();
            }
        }
        resultsChecked = true;
    }
    private void UpdateTotalPrizeValue()//Update the total winnings text
    {
        if (totalPrizeText != null)
        {
            totalPrizeText.text = "TOTAL: " + totalPrizeValue;
        }
    }
    public int GetTotalPrizeValue()//get the total prize value
    {
        return totalPrizeValue;
    }
    public void AdjustTotalPrize(int amount)//adjust the total prize value
    {
        totalPrizeValue += amount;
        UpdateTotalPrizeValue();
    }
    public void StartSpinFromBet()//Start spinning the reels from the betting UI
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine(PullHandle());
        }
    }
    private IEnumerator PlayJackPot()
    {   // Freeze game logic
        Time.timeScale = 0f; // Stop all movement/physics
        bettingUI.gameObject.SetActive(false);//hiding the betting UI
        jackpotText.gameObject.SetActive(true);//showing the jackpot text
        jackpotText.text = "JACKPOT!!!";
        AudioSource bgMusic = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        if (bgMusic != null) bgMusic.Pause();// Pause background music


        audioSource.clip = jackpotClip;//Play the jackpot sound
        audioSource.Play();

        yield return new WaitForSecondsRealtime(audioSource.clip.length);//wait for the jackpot sound to finish
        if (bgMusic != null) bgMusic.UnPause();
        Time.timeScale = 1f; // Resume game logic
        jackpotText.gameObject.SetActive(false);//hiding the jackpot text
        bettingUI.gameObject.SetActive(true);//showing the betting UI
        resultsChecked = true;
    }

    // Play win sound effect
    private void PlayWinSound()
    {
        if (audioSource != null && winClip != null)
        {
            audioSource.PlayOneShot(winClip);
        }
    }

    // Reset Slot Machine
    public void ResetSlotMachine()
    {
        totalPrizeValue = 30;
        prizeValue = 0;
        UpdateTotalPrizeValue();
        isFirstSpin = true;
        resultsChecked = false;
        prizeText.enabled = false;

        foreach (Row row in rows)
    {
        row.ResetRow();// Reset each row
    }
        
    }
}