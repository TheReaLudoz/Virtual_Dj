using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Button3D : MonoBehaviour
{
    public Action OnButtonPressed;

    public void Press()
    { 
        if (OnButtonPressed != null)
        {
            Debug.Log("bottone_premuto");
            OnButtonPressed();
        }
        
    }
}
