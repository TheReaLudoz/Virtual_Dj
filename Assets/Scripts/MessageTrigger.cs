using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigging : MonoBehaviour
{
    // Start is called before the first frame update
    public Message[] messages;
    public Actor[] actors;

    public void StartSialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
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