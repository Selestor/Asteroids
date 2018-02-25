using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsThrustDownScript : MonoBehaviour
{
    private bool isThrustDownPressed = false;

    public void ToggleThrustDownButton()
    {
        if (isThrustDownPressed) isThrustDownPressed = false;
        else isThrustDownPressed = true;
    }

    void Update()
    {
        if (isThrustDownPressed)
        {
            GameManager.instance.playerInstance.GetComponent<PlayerScript>().ThrustDown();
        }
    }
}
