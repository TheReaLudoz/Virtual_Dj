using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Button3DSlider))]
public class Button3DSliderInteractable : Interactable
{
    private Button3DSlider button3DSlider;

    // Use this for initialization
    void Start()
    {
        button3DSlider = gameObject.GetComponent<Button3DSlider>();

    }

    public override void Interact(GameObject caller)
    {
        button3DSlider.Slide();
        Debug.Log("Bottone slidato");
    }
}
