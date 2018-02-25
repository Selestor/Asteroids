using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsRotateLeftScript : MonoBehaviour
{
    private bool isRotateLeftPressed;

    public void ToggleRotateLeftButton()
    {
        if (isRotateLeftPressed) isRotateLeftPressed = false;
        else isRotateLeftPressed = true;
    }

    void Update()
    {
        if (isRotateLeftPressed)
        {
            GameManager.instance.playerInstance.GetComponent<PlayerScript>().RotateLeft();
        }
    }
}
