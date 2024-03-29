using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandikKontrol : MonoBehaviour
{
    Animator Anim;

    int kacinciVurus;

    [SerializeField]
    GameObject parlamaEfekti;

    [SerializeField]
    GameObject coinPrefab;

    Vector2 patlamaMiktari = new Vector2(1, 4);

    private void Awake()
    {
        kacinciVurus = 0;
        Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("kilicVurusBox"))
        {
            if (kacinciVurus == 0)
            {
                Anim.SetTrigger("sallanma");
                Instantiate(parlamaEfekti,transform.position,transform.rotation);
            }
            else if(kacinciVurus == 1)
            {
                Anim.SetTrigger("sallanma");
                Instantiate(parlamaEfekti, transform.position, transform.rotation);
            }
            else
            {
                Anim.SetTrigger("parcalanma");

                GetComponent<BoxCollider2D>().enabled = false;  

                for (int i = 0; i < 3; i++)
                {
                    Vector3 rastgeleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);
                    GameObject coin = Instantiate(coinPrefab, rastgeleVector, transform.rotation);
                    coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));
                }
            }
            kacinciVurus++;
        }
    }
}
