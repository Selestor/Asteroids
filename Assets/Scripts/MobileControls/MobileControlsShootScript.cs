using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsShootScript : MonoBehaviour
{
    public void Shoot()
    {
        GameManager.instance.playerInstance.GetComponent<PlayerScript>().Shoot();
    }
}
