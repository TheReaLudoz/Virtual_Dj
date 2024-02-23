using UnityEngine;

public class ComputerInteractable : Interactable
{
    public override void Interact(GameObject caller)
    {
        // Implementa qui il comportamento dell'interazione con il computer
        Debug.Log("Interacting with the computer");
    }
}
