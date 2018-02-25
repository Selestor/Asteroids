using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsRotateRightScript : MonoBehaviour
{
    private bool isRotateRightPressed;

    public void ToggleRotateRightButton()
    {
        if (isRotateRightPressed) isRotateRightPressed = false;
        else isRotateRightPressed = true;
    }

    void Update()
    {
        if (isRotateRightPressed)
        {
            GameManager.instance.playerInstance.GetComponent<PlayerScript>().RotateRight();
        }
    }
}
