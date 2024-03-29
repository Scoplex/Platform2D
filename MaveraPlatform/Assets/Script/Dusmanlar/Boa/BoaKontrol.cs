using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoaKontrol : MonoBehaviour
{
    [SerializeField]
    float boaYurumeHizi, boaKosmaHizi;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    float gorusMesafesi = 8f;

    [SerializeField]
    BoxCollider2D boaCollider;

    public bool oldumu;

    public LayerMask playerLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        oldumu = false;
    }

    private void Update()
    {
        if (oldumu)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.left),gorusMesafesi,playerLayer);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.green);

        transform.localScale = new Vector3(-1, 1, 1);
        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector2(-boaKosmaHizi, rb.velocity.y);
                anim.SetBool("kossunmu",true);
            }
        }
        else
        {
            rb.velocity = new Vector2(-boaYurumeHizi, rb.velocity.y);
            anim.SetBool("kossunmu", false);

        }

    }

    public void boaOldu()
    {
        oldumu = true;
        anim.SetTrigger("canverdi");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            box.enabled = false;
        }
        Destroy(gameObject,4f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boaCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("atakYapti");
                other.GetComponent<PlayerHareketKontrol>().GeriTepki();
                other.GetComponent<PlayerCanKontrol>().CaniAzalt();
            }
        }
    }

}
