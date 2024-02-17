using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.OpenDialogue(messages, actors);
        }
        else
        {
            Debug.LogError("DialogueManager non trovato!");
        }
    }
}
[System.Serializable]
public class Message{
    public int actorId;
    public string message;
}
[System.Serializable]
public class Actor{
    public string name;
    public Sprite sprite;
}