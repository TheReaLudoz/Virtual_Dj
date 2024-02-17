using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string dialogueMessage = "Ciao, sono un robot!";
    private DialogueSystem dialogueSystem;

    private void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystem non trovato nella scena!");
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro del mouse
        {
            if (dialogueSystem != null)
            {
                dialogueSystem.ShowDialogue(dialogueMessage);
            }
        }
    }
}
