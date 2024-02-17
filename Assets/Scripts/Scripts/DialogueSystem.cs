using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialoguePanel.SetActive(true);
    }
}
