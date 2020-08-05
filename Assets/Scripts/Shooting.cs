using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject[] bullet;
    public Transform player;

    bool isShooting = false;

    int powerLevel = 1;
    public float power = 1;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (isShooting == false && PlayerMovement.isDead == false)
            {
                Power();
            }
        }
    }

    void Power()
    {
        switch (powerLevel)
        {
            case 1:
                StartCoroutine(Shoot());
                break;

            case 2:
                StartCoroutine(Shoot2());
                break;
        }
    }

    IEnumerator Shoot()
    {
        Instantiate(bullet[0], player);
        isShooting = true;
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(0.05f);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        isShooting = false;
    }

    IEnumerator Shoot2()
    {
        Instantiate(bullet[1], player);
        isShooting = true;
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(0.05f);
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
        }
        isShooting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup")){
            Destroy(other.gameObject);
            power++;

            if(power <= 10)
            {
                powerLevel = 1;
            }
            if(power >= 11 && power <= 25)
            {
                power = 2;
            }
            if(power >= 51 && power <= 75)
            {
                power = 3;
            }
        }
    }
}
