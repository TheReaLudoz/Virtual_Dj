using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Button3D))]
public class Button3DInteractable : Interactable
{
    private Button3D button3D;

    // Use this for initialization
    void Start()
    {
        button3D = gameObject.GetComponent<Button3D>(); 

    }

    public override void Interact(GameObject caller)
    {
    }

    void OnMouseDown()
    {
        button3D.Press();
        Debug.Log("Bottone Premuto");
    }
}
