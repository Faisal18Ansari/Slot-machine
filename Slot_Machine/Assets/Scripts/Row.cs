using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Row : MonoBehaviour
{
    private int randomValue;//decides how long a particular row will spin
    private float timeInterval;//used to slow the movement of the row down during the spinning
    public string stoppedSlot;//String representing the symbol where the row stopped
    public bool rowStopped;//Boolean to check if the row has stopped spinning
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rowStopped = true;
        SlotMachine.HandlePulled += StartRotating;// Subscribe to the HandlePulled event
    }
    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");// Start the rotation coroutine
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;// Initial time interval for spinning
        for (int i = 0; i < 8; i++)// one full pass across all 8 positions (2..9)
        {
            StepDownOneCell();
            yield return new WaitForSeconds(timeInterval);//smooth operation
        }
        int randomSteps = Random.Range(24, 48);// 3–6 full cycles at 8 cells per cycle
        for (int i = 0; i < randomSteps; i++)
        {
            StepDownOneCell();
            // slowdown by increasing the wait interval at milestones
            if (i > Mathf.RoundToInt(randomSteps * 0.25f)) timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomSteps * 0.50f)) timeInterval = 0.10f;
            if (i > Mathf.RoundToInt(randomSteps * 0.75f)) timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomSteps * 0.92f)) timeInterval = 0.20f;

            yield return new WaitForSeconds(timeInterval);//smooth operation
        }   
        // check all the positions for symbols recognition
        if (transform.position.y == 2f)
        {
            stoppedSlot = "Seven";
        }
        else if (transform.position.y == 3f)
        {
            stoppedSlot = "Bell";
        }
        else if (transform.position.y == 4f)
        {
            stoppedSlot = "Bar";
        }
        else if (transform.position.y == 5f)
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.position.y == 6f)
        {
            stoppedSlot = "Bell";
        }
        else if (transform.position.y == 7f)
        {
            stoppedSlot = "Bar";
        }
        else if (transform.position.y == 8f)
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.position.y == 9f)
        {
            stoppedSlot = "Seven";
        }
        rowStopped = true;//row has stopped spinning
    }
    // Moves down by STEP and wraps from TOp -> BOTTOM seamlessly
    private void StepDownOneCell()
    {
        Vector2 pos = transform.position;

        // Move down one step
        pos.y += 1f;

        // Wrap from below the lowest position back to the top
        if (pos.y <= 2f)
        {
            pos.y = 9f;
        }

        // Wrap from above the highest position back to the bottom
        if (pos.y >= 9f)
        {
            pos.y = 2f;
        }

        transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        SlotMachine.HandlePulled -= StartRotating;// Unsubscribe from the HandlePulled event
    }
    public void ResetRow()
    {
        // Resetting the position to the starting point
        transform.position = new Vector2(transform.position.x, 7f);

        stoppedSlot = "";
        rowStopped = true;
    }
}
