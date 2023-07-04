using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool talking = false;
    public bool canSkip = false;
    public bool canStart = true;
    public bool waitingNextDialogue= false;
    public float waitingTimeSkip = 0.5f;
    public float waitingTimeNextDialogue= 1;
    public float timerNextDialogue;
    public GameObject interactButton;
    public DialogueManager dialogueManager;

    public Dialogue dialogue;
    private void Start()
    {
        timerNextDialogue = waitingTimeNextDialogue;
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void Update()
    {
        if (dialogueManager.isWriting)
        {
            waitingTimeSkip -= Time.deltaTime;
            if (waitingTimeSkip <= 0)
                canSkip = true;
        }
        else
        {
            waitingTimeSkip = 0.5f;
            canSkip = false;
        }
        if (waitingNextDialogue)
        {
            waitingTimeNextDialogue -= Time.deltaTime;
            if(waitingTimeNextDialogue <= 0)
            {
                canStart = true;
                waitingTimeNextDialogue = timerNextDialogue;
                waitingNextDialogue = false;
            }
        }
    }
    public void Dialogue()
    {
        if (!dialogueManager.isWriting)
        {
            if (talking)
            {
                DisplaySentences();
                if (dialogueManager.endDialogue)
                {
                    canStart = false;
                    talking = false;
                    waitingNextDialogue = true;
                }
            }
            else
            {
                if (canStart)
                {
                    DialogueStart();
                    talking = true;
                }
            }
        }
        else
        {
            if(canSkip)
            {
                SkipDialogue();
            }
        }
    }
    public void DialogueStart()
    {
        dialogueManager.StartDialogue(dialogue);
        dialogueManager.DisplaySentences();
    }
    public void DisplaySentences()
    {
        dialogueManager.DisplaySentences();
    }
    void SkipDialogue()
    {
        dialogueManager.Skip();
    }
}
