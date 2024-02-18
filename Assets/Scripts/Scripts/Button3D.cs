using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Button3D : MonoBehaviour
{
    public Action OnButtonPressed;
    public float localYFinalPressedPos;
    public float pressDuration = 0.3f;
    public float releaseDuration = 0.1f;

    public Color unpressedColor;
    public Color pressedColor;

    private MeshRenderer renderer;
    private bool isPressed = false;

    public void Press()
    { 
        if (OnButtonPressed != null)
        {
            Debug.Log("bottone_premuto");
            OnButtonPressed();
        }
        
    }
}
