using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHP : MonoBehaviour
{
    [Header("Health Settings")]
    public float hp;
    public float damage;
    public GameObject turret;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            if(hp <= 0)
            {
                Destroy(turret);
            }
        }
    }
}
