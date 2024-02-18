using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Button3DSlider : MonoBehaviour
{
    public Action OnButtonSlide;
    private MeshRenderer renderer;
    private bool isSlide = false;
    public void Slide()
    {
        if (OnButtonSlide != null)
        {
            Debug.Log("bottone_slidato");
            OnButtonSlide();
        }

        // Muovi il bottone lungo l'asse Y
        //float deltaZ = 0.1f; // Regola il valore di spostamento Y
        //transform.position += new Vector3(0, 0, deltaZ);
    }

}
