using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController2 : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _mouseSensitivity = 100f;

    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
    }


    void Update()
    {
       

        Cursor.lockState = CursorLockMode.None;

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;


    }

 
}