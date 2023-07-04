using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public float letterSpeed=0.1f;
    public bool endDialogue = false;
    public bool isWriting = false;

    public GameObject dialogueBox;
    public Animator animator;

    public Image NPCImage;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI NPCName;

    public Queue<string> sentences;
    private void Start()
    {
        if (dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(false);
        }
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        endDialogue = false;
        dialogueBox.SetActive(true);
        animator.SetBool("Open", true);
        NPCName.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        { 
            sentences.Enqueue(sentence); 
        }
    }
    public void DisplaySentences()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type(sentence));
    }
    void EndDialogue()
    {
        animator.SetBool("Open", false);
        StartCoroutine(Close());
        endDialogue = true;
    }
    IEnumerator Type(string sentence)
    {
        letterSpeed = 0.1f;
        dialogueText.text = string.Empty;
        foreach(char letter in sentence.ToCharArray())
        {
            isWriting = true;
            dialogueText.text += letter;
            if(letterSpeed >0)
                yield return new WaitForSeconds(letterSpeed);
        }
        isWriting = false;
    }
    public void Skip()
    {
        letterSpeed = 0;
    }
    IEnumerator Close()
    {
        float closeWaitTime = 1f;
        yield return new WaitForSeconds(closeWaitTime);
        dialogueBox.SetActive(false);
    }
}

