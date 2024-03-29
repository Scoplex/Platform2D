using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHareketKontrol : MonoBehaviour
{
    public static PlayerHareketKontrol instance;
    Rigidbody2D rb;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    GameObject normalPlayer, kilicPlayer, mizrakPlayer, okPlayer;

    [SerializeField]
    Animator normalAnim,kilicAnim,mizrakAnim, okAnim;

    [SerializeField]
    SpriteRenderer normalSprite,kilicSprite,mizrakSprite,okSprite;

    [SerializeField]
    GameObject kilicVurusBoxObje;


    public LayerMask zeminMaske;
    public float hareketHizi;
    public float ziplamaGucu;

    bool zemindemi;
    bool ikinciKezZiplasinMi;
    public bool playerCanVerdimi;
    bool kiliciVurduMu;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;

    float geriTepkiSayaci;  

    [SerializeField]
    GameObject atilacakMizrak;

    [SerializeField]
    Transform mizrakCikisNoktasi;

    bool yonSagdami;

    [SerializeField]
    GameObject atilacakOk;

    [SerializeField]
    Transform okCikisNoktasi;

    bool okAtabilirmi;

    [SerializeField]
    float tirmanisHizi = 3f;

    [SerializeField]
    GameObject normalKamera, kilicKamera, okKamera,mizrakKamera;

    private void Awake()
    {
        okAtabilirmi = true;
        kiliciVurduMu = false;
        playerCanVerdimi = false;
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        kilicVurusBoxObje.SetActive(false);
    }

    private void Update()
    {
        if (playerCanVerdimi == true)
        {
            return;
        }
        if (geriTepkiSayaci <= 0) 
        {
            HareketEt();
            ziplaFNC();
            yonuDegistir();
            if (normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }
            if(kilicPlayer.activeSelf)
            {
                kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 1f);
            }

            if (mizrakPlayer.activeSelf)
            {
                mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite .color.b, 1f);
            }
            if (okPlayer.activeSelf)
            {
                okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, 1f);
            }

            if (Input.GetMouseButtonDown(0) && kilicPlayer.activeSelf)
            {
                kiliciVurduMu = true; 
                kilicVurusBoxObje.SetActive(true);
                SesManager.instance.sesEfektiCikar(4);
            }
            else
            {
                kiliciVurduMu = false;
            }

            if (Input.GetKeyDown(KeyCode.W) && mizrakPlayer.activeSelf)
            {
                mizrakAnim.SetTrigger("mizrakAtti");
                Invoke("MizragiFirlat", .5f);
                SesManager.instance.sesEfektiCikar(5);

            }

            if(Input.GetKeyDown(KeyCode.E) && okPlayer.activeSelf && okAtabilirmi)
            {
                okAnim.SetTrigger("okAtti");
                StartCoroutine(OkuAzSonraAt());
                SesManager.instance.sesEfektiCikar(7);
            }

            if (okPlayer.activeSelf)
            {
                if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("TirmanmaLayer")))
                {
                    float h = Input.GetAxis("Vertical");
                    Vector2 tirmanisVector = new Vector2(rb.velocity.x, h * tirmanisHizi);
                    rb.velocity = tirmanisVector;
                    rb.gravityScale = 0f;
                    okAnim.SetBool("tirmansinmi", true);
                    okAnim.SetFloat("yukariHareketHizi", Mathf.Abs(rb.velocity.y));
                }
                else
                {
                    okAnim.SetBool("tirmansinmi", false);
                    rb.gravityScale = 5f;
                }
            }
        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;

            if (yonSagdami)
            {
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(geriTepkiGucu, rb.velocity.y);
            }
        }




        if (normalPlayer.activeSelf)
        {
            normalAnim.SetBool("animzemindemi", zemindemi);
            normalAnim.SetFloat("animhareketHizi", Mathf.Abs(rb.velocity.x));
        }
        if(kilicPlayer.activeSelf)
        {
            kilicAnim.SetBool("animzemindemi", zemindemi);
            kilicAnim.SetFloat("animhareketHizi", Mathf.Abs(rb.velocity.x));
        }

        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetBool("animzemindemi", zemindemi);
            mizrakAnim.SetFloat("animhareketHizi", Mathf.Abs(rb.velocity.x));
        }
        if (okPlayer.activeSelf)
        {
            okAnim.SetBool("animzemindemi", zemindemi);
            okAnim.SetFloat("animhareketHizi", Mathf.Abs(rb.velocity.x));
        }


        if (kiliciVurduMu && kilicPlayer.activeSelf)
        {
            kilicAnim.SetTrigger("kiliciVurdu");

        }
    }

    void HareketEt()
    {
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * hareketHizi, rb.velocity.y);
    }

    void yonuDegistir()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            yonSagdami = true;
            //* new vector3(1,1,1); ile Vector3.one; ayný þeyi ifade ediyor.
        }
    }

    void ziplaFNC()
    {
        zemindemi= Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminMaske);

        if (Input.GetButtonDown("Jump") && (zemindemi || ikinciKezZiplasinMi))
        {
            if (zemindemi)
            {
                ikinciKezZiplasinMi = true;
            }
            else
            {
                ikinciKezZiplasinMi = false;
            }
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);
        }
    }


    public void GeriTepki()
    {
        geriTepkiSayaci = geriTepkiSuresi;


        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 0.5f);
        }
        if (kilicPlayer.activeSelf)
        {
            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 0.5f);
        }
        if (mizrakPlayer.activeSelf)
        {
            mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, 0.5f);
        }
        if (okPlayer.activeSelf)
        {
            okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, 0.5f);
        }

        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void PlayerCanVerdi()
    {
        rb.velocity = Vector2.zero;
        playerCanVerdimi = true;
        if (normalPlayer.activeSelf)
        {
            normalAnim.SetTrigger("canVerdi");
        }
        if(kilicPlayer.activeSelf)
        {
            kilicAnim.SetTrigger("canVerdi");
        }
        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetTrigger("canVerdi");
        }
        if (okPlayer.activeSelf)
        {
            okAnim.SetTrigger("canVerdi");
        }


        StartCoroutine(PlayerYokEtSahneYenile());
    }
    IEnumerator PlayerYokEtSahneYenile()
    {
        yield return new WaitForSeconds(.2f);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void HerseyiKapatKiliciAc()
    {
        TumKameralariKapat();
        kilicKamera.SetActive(true);
        normalPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        kilicPlayer.SetActive(true);
        okPlayer.SetActive(false);
    }

    public void herseyiKapatMizrakAc()
    {
        TumKameralariKapat();
        mizrakKamera.SetActive(true);
        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(true);
        okPlayer.SetActive(false);
    }

    public void HerseyiKapatNormaliAc()
    {
        TumKameralariKapat();
        normalKamera.SetActive(true);
        normalPlayer.SetActive(true);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(false);
    }
    public void HerseyiKapatOkuAc()
    {
        TumKameralariKapat();
        okKamera.SetActive(true);
        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(true);
    }
    
    void TumKameralariKapat()
    {
        normalKamera.SetActive(false);
        kilicKamera.SetActive(false);
        okKamera.SetActive(false);
    }
    void MizragiFirlat()
    {
        GameObject mizrak = Instantiate(atilacakMizrak, mizrakCikisNoktasi.position, mizrakCikisNoktasi.rotation);
        mizrak.transform.localScale = transform.localScale;
        mizrak.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right * transform.localScale.x * 7f;
        Invoke("HerseyiKapatNormaliAc", .1f);
    }

    IEnumerator OkuAzSonraAt()
    {
        okAtabilirmi = false;
        yield return new WaitForSeconds(.7f);
        OkPoolManager.instance.OkuFirlat(okCikisNoktasi, this.transform);
        okAtabilirmi = true;
    }


   // void okuFirlat()
    
      //  OkPoolManager.instance.OkuFirlat(okCikisNoktasi,this.transform);

       // GameObject okObje = Instantiate(atilacakOk, okCikisNoktasi.position, okCikisNoktasi.rotation);
       // okObje.transform.localScale = transform.localScale;
       // okObje.GetComponent<Rigidbody2D>().velocity = okCikisNoktasi.right * transform.localScale.x * 15f;
    

    public void playeriHareketsizYap()
    {
        if (normalPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            normalAnim.SetFloat("animhareketHizi", 0f);
        }

        if (kilicPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            kilicAnim.SetFloat("animhareketHizi", 0f);
        }

        if (mizrakPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            mizrakAnim.SetFloat("animhareketHizi", 0f);
        }
        if (okPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            okAnim.SetFloat("animhareketHizi", 0f);
        }
    }
}
