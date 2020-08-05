using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    bool isEnabled;
    public GameObject eventSys;
    public GameObject player;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (isEnabled == true)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }
    }

    public void Activate()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Shooting>().enabled = true;
        eventSys.SetActive(false);
        isEnabled = true;
    }

    public void Deactivate()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
        eventSys.SetActive(true);
        isEnabled = false;
    }
}
