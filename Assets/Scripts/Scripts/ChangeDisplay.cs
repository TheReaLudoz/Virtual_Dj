using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDisplay : MonoBehaviour
{
    [SerializeField] private CanvasController _canvasController1;
    [SerializeField] private CanvasController _canvasController2;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    

    void Update()
    {
        if (Input.GetKeyDown("1"))
        { 
            _canvasController1.CloseCanvas();
            _canvasController2.CloseCanvas();
            CameraOne();
        }

        if (Input.GetKeyDown("2"))
        {
            _canvasController1.CloseCanvas();
            _canvasController2.CloseCanvas();
            CameraTwo();
        }

         if (Input.GetKeyDown("3"))
        {
            _canvasController1.OpenCanvas();
            _canvasController2.OpenCanvas();
            CameraThree();
        }
    }

    void CameraOne()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
    }

    void CameraTwo()
    {
        Camera2.SetActive(true);
        Camera1.SetActive(false);
        Camera3.SetActive(false);
    }
    void CameraThree()
        {
            Camera3.SetActive(true);
            Camera1.SetActive(false);
            Camera2.SetActive(false);
        }

}