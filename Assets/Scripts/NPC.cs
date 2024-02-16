using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueTrigger trigger;
    private void OnMouseDown()
    {
        // Controlla se il pulsante sinistro del mouse è stato premuto
        if (Input.GetMouseButtonDown(0))
        {trigger.StartDialogue();
        }
    }
}
