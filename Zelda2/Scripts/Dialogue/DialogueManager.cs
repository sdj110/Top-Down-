using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    #region Singleton
    public static DialogueManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    // Attributes
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private bool m_Talking;

    private Queue<string> m_Sentences;

    public bool Talking
    {
        get
        {
            return m_Talking;
        }

        set
        {
            m_Talking = value;
        }
    }

    private void Start()
    {
        m_Talking = false;
        m_Sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Set talking to true
        m_Talking = true;

        // Update Animator to show dialogue box
        animator.SetBool("IsOpen", true);

        // Set name text to triggers name 
        nameText.text = dialogue.name;

        // Clear old sentences from last trigger
        m_Sentences.Clear();

        // Create queue with sentences
        foreach (string sentence in dialogue.sentences)
        {
            m_Sentences.Enqueue(sentence);
        }
        // display first message
        DisplayNextSentence();
    }

    // Dequeue sentence and display on screen
    public void DisplayNextSentence()
    {
        // check if we are at the end
        if (m_Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Display next sentence
        string sentence = m_Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    // When we reach end of dialogue
    private void EndDialogue()
    {
        // End talking 
        m_Talking = false;
        // Update Animator to close the dialogue box
        animator.SetBool("IsOpen", false);
    }

}
