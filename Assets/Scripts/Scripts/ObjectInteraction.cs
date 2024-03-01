using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : Interactable
{
    public IlluminationController illuminationController;

    // Dichiarazione di un delegate per gestire l'evento di clickare su un oggetto
    public delegate void ObjectPressedDelegate(GameObject pressedObject);
    // Evento statico associato al delegate per notificare il click su un oggetto
    public static event ObjectPressedDelegate OnObjectPressed;

    // Implementa la funzione Interact ereditata da Interactable
    public override void Interact(GameObject caller)
    {
        OnObjectPressed?.Invoke(gameObject);
        Debug.Log("Evento OnObjectPressed invocato");
    }
}
