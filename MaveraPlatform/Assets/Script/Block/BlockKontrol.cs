using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKontrol : MonoBehaviour
{
    public Transform altPoint;

    Animator anim;

    Vector3 hareketYonu = Vector3.up;
    Vector3 orijinalPoz;
    Vector3 animPoz;

    public LayerMask playerLayer;   

    bool animasyonbaslasinmi;
    bool hareketEtsinmi=true;

    public GameObject coinPrefab;
    Vector3 coinPoz;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        orijinalPoz = transform.position;
        animPoz = transform.position;
        animPoz.y += 0.15f;
        coinPoz = transform.position;
        coinPoz.y += 1f;
    }

    private void Update()
    {
        CarpismayiKontrolEt();
        AnimasyonuBaslat(); 
    }
    void CarpismayiKontrolEt()
    {
        if (hareketEtsinmi)
        {
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, .1f, playerLayer);

            if (hit && hit.collider.gameObject.tag == "Player")
            {
                anim.Play("mat");
                animasyonbaslasinmi = true;
                hareketEtsinmi = false;

                Instantiate(coinPrefab, coinPoz, Quaternion.identity);
            }
        }
    }

    void AnimasyonuBaslat()
    {
        if (animasyonbaslasinmi)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);
            if (transform.position.y >= animPoz.y)
            {
                hareketYonu = Vector3.down; 
            }
            else if (transform.position.y<=orijinalPoz.y)
            {
                animasyonbaslasinmi = false;
            }
        }
    }
}
