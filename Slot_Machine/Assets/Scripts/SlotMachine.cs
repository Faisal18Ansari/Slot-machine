using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
public class SlotMachine : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };//stating that a global event has been created and calling out other scripts
    public TextMeshProUGUI prizeText;//text component to display the prize
    public TextMeshProUGUI totalPrizeText;//text component to display the total prize
    public Row[] rows;//array of rows to hold the symbols
    private int prizeValue;//value of the current prize
    private int totalPrizeValue; // Running total prize
    private bool resultsChecked = false;//boolean to check if results have been checked
    public Sprite normalSprite;//access to normal sprite
    public Sprite downSprite;//access to down sprite
    private SpriteRenderer spriteRenderer;//sprite renderer for the handle
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//getting the sprite renderer component
        spriteRenderer.sprite = normalSprite;
        totalPrizeValue = 0;
        UpdateTotalPrizeValue();
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
            totalPrizeValue += prizeValue; // Add current prize to total
            UpdateTotalPrizeValue();          // Refresh UI
            prizeText.enabled = true;
            prizeText.text = "PRIZE: " + prizeValue;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnMouseDown();// Call OnMouseDown when the mouse button is pressed
        }
    }
    private void OnMouseDown()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine(PullHandle());
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
}
