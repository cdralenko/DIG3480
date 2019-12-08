using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickup : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag ("Pickup"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
