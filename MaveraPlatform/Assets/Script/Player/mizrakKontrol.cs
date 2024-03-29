using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mizrakKontrol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boa") && !other.GetComponent<BoaKontrol>().oldumu); 
        {
            Destroy(gameObject);
            other.GetComponent<BoaKontrol>().boaOldu();

        }   
    }
}
