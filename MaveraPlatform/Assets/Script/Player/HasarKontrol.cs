using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarKontrol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCanKontrol.instance.CaniAzalt();
            PlayerHareketKontrol.instance.GeriTepki();
        }
    }
}
