using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletHealtKontrol : MonoBehaviour
{
    public int maxSaglik;

    int gecerliSaglik;

    public bool iskeletOlduMu;

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        iskeletOlduMu = false;
    }

    public void caniAzalt()
    {
        gecerliSaglik--;
        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            iskeletOlduMu = true;
            GetComponent<Animator>().SetTrigger("caniniVerdi");
            GetComponent<BoxCollider2D>().enabled = false;
            iskeletSpawnKoontrol.instance.ListeyiAzalt(this.gameObject);
            Destroy(gameObject, .3f);
        }
    }
}
