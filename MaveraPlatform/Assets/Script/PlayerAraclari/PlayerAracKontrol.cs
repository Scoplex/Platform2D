using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAracKontrol : MonoBehaviour
{
    [SerializeField]
    bool kilicmi, mizrakmi, okmu;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other != null && kilicmi)
            {
                other.GetComponent<PlayerHareketKontrol>().HerseyiKapatKiliciAc();
                Destroy(gameObject);
            }

            if (other != null && mizrakmi)
            {
                other.GetComponent<PlayerHareketKontrol>().herseyiKapatMizrakAc();

                Destroy(gameObject);
            }

            if (other != null && okmu) 
            {
                other.GetComponent<PlayerHareketKontrol>().HerseyiKapatOkuAc();

                Destroy(gameObject);
            }
        }
    }
}
