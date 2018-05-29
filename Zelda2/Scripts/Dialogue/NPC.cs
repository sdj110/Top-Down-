using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    DialogueManager m_DialogueManager;

    private void Start()
    {
        m_DialogueManager = DialogueManager.Instance;
    }

    public void TriggerDialogue()
    {
        m_DialogueManager.StartDialogue(dialogue);
    }
}