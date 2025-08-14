using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Row : MonoBehaviour
{
    private int randomValue;//decides how long a particular row will spin
    private float timeInterval;//used to slow the movement of the row down during the spinning
    public string stoppedSlot;
    public bool rowStopped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rowStopped = true;
        SlotMachine.HandlePulled += StartRotating;
    }
    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;
        for (int i = 0; i < 8; i++)// one full pass across all 8 positions (-4..3)
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
        if (transform.position.y == -4f)
        {
            stoppedSlot = "Seven";
        }
        else if (transform.position.y == -3f)
        {
            stoppedSlot = "Bell";
        }
        else if (transform.position.y == -2f)
        {
            stoppedSlot = "Bar";
        }
        else if (transform.position.y == -1f)
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.position.y == 0f)
        {
            stoppedSlot = "Bell";
        }
        else if (transform.position.y == 1f)
        {
            stoppedSlot = "Bar";
        }
        else if (transform.position.y == 2f)
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.position.y == 3f)
        {
            stoppedSlot = "Seven";
        }
        rowStopped = true;//row has stopped spinning
    }
    // Moves down by STEP and wraps from TOp -> BOTTOM seamlessly
    private void StepDownOneCell()
    {
        Vector2 pos = transform.position;
        // If we've reached/passed the top boundary, jump just above bottom
        // so that after subtracting STEP we land exactly on bottom.
        if (pos.y <= -4f)
        {
            pos.y = 3f + 1f; // 1f is the height of one cell
        }
        pos.y -= 1f;
        transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        SlotMachine.HandlePulled -= StartRotating;
    }
}
