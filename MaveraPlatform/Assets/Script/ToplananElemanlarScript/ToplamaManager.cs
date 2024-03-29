using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{
    [SerializeField]
    bool coinmi,iksirmi;

    [SerializeField]
    GameObject patlamaEfekti;

    bool toplandimi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !toplandimi)
        {

            if (coinmi)
            {
                toplandimi = true;
                GameManager.instance.toplananCoinAdet++;
                UIManager.instance.AdetGuncelle();
                Destroy(gameObject);
                Instantiate(patlamaEfekti, transform.position, Quaternion.identity);
                SesManager.instance.KarisikSesEfektiCikar(6);
            }

            if (iksirmi)
            {
                toplandimi = true;
                PlayerCanKontrol.instance.CaniArttir();
                Destroy(gameObject);
                Instantiate(patlamaEfekti, transform.position, Quaternion.identity);
            }
        }
    }
}
