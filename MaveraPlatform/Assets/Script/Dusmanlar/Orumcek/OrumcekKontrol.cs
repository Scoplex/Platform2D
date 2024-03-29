    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
public class OrumcekKontrol : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    [SerializeField]
    Slider orumcekSlider;

    [SerializeField]
    GameObject iksirPrefab;


    public int maxSaglik;
    int gecerliSaglik;
    public float orumcekHizi;
    public float beklemeSuresi;
    float beklemeSayac;
    Animator anim;
    int kacinciPozisyon;
    public float takipMesafesi= 5f;
    BoxCollider2D orumcekCollider;
    Transform hedefPlayer;
    bool atakYapabilirmi;
    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        orumcekCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sliderGuncelle();
        orumcekSlider.maxValue = maxSaglik;
        gecerliSaglik = maxSaglik;
        atakYapabilirmi = true;
        hedefPlayer = GameObject.Find("Player").transform;

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
    }
    private void Update()
    {

        if (atakYapabilirmi == false)
            return;


        if (beklemeSayac > 0)
        {
            // ORUMCEK VERÝLEN NOKTADA DURUYOR
            beklemeSayac-=Time.deltaTime;
            anim.SetBool("hareketEtsinmi", false);
        }
        else
        {
            if(hedefPlayer.position.x > pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
            {
                Vector3 yeniPos = hedefPlayer.position;
                yeniPos.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, yeniPos, orumcekHizi * Time.deltaTime);
                anim.SetBool("hareketEtsinmi", true);

                if (transform.position.x > hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < hedefPlayer.position.x)
                {
                    transform.localScale = Vector3.one;
                }

            }
            else
            {
                anim.SetBool("hareketEtsinmi", true);

                if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = Vector3.one;
                }

                transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, orumcekHizi * Time.deltaTime);

                if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    pozisyonuDegistir();
                }
            }
        }
    }

    void pozisyonuDegistir()
    {
        kacinciPozisyon++;

        if (kacinciPozisyon >= pozisyonlar.Length)
            kacinciPozisyon = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && atakYapabilirmi)
        {
            atakYapabilirmi = false;
            anim.SetTrigger("atakYapti");
            other.GetComponent<PlayerHareketKontrol>().GeriTepki();
            other.GetComponent<PlayerCanKontrol>().CaniAzalt();

            StartCoroutine(YenidenAtakYapsin());
        }   
    }

    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);
        if (gecerliSaglik > 0)
        {
            atakYapabilirmi = true;
        }
    }

    public IEnumerator GeriTepkiOrumcek()
    {
        atakYapabilirmi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);
        gecerliSaglik--;
        sliderGuncelle();
        if (gecerliSaglik <= 0)
        {
            Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            atakYapabilirmi = false;
            gecerliSaglik = 0;
            anim.SetTrigger("canVerdi");
            orumcekCollider.enabled = false;
            orumcekSlider.gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);
                yield return new WaitForSeconds(0.05f);
            }

            anim.SetBool("hareketEtsinmi", false);

            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            atakYapabilirmi = true;
        }

    }

    void sliderGuncelle()
    {
        orumcekSlider.value = gecerliSaglik;
    }

}
