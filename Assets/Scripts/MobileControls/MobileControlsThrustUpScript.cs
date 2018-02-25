using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsThrustUpScript : MonoBehaviour
{
    private bool isThrustUpPressed = false;

    public void ToggleThrustUpButton()
    {
        if (isThrustUpPressed) isThrustUpPressed = false;
        else isThrustUpPressed = true;
    }

    void Update()
    {
        if (isThrustUpPressed)
        {
            GameManager.instance.playerInstance.GetComponent<PlayerScript>().ThrustUp();
        }
    }
}
