using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletSpawnKoontrol : MonoBehaviour
{
    public static iskeletSpawnKoontrol instance;

    [SerializeField]
    GameObject iskeletPrefab;

    public List<GameObject> iskeletList = new List<GameObject>();

    [SerializeField]
    float spawnSuresi;

    float spawnSayac;

    int iskeletAdet;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (iskeletList.Count > 0)
        {
            iskeletList.Clear();

            iskeletAdet = 0;
        }
        StartCoroutine(iskeletSpawnRoutine());
    }

    IEnumerator iskeletSpawnRoutine()
    {
        yield return new WaitForSeconds(spawnSuresi);

        spawnSayac = 0;
        if (iskeletList.Count < 3)
        {
            while(spawnSayac<spawnSuresi)
            {
                spawnSayac += Time.deltaTime;
                yield return null;
            }

            GameObject iskelet = Instantiate(iskeletPrefab, transform.position, Quaternion.identity);
            iskelet.transform.SetParent(transform);
            iskeletList.Add(iskelet);
            iskeletAdet++;

            if (spawnSayac >= spawnSuresi && iskeletAdet<6)
            {
                StartCoroutine(iskeletSpawnRoutine());
            }
        }

    }

    public void ListeyiAzalt(GameObject iskelet)
    {
        iskeletList.Remove(iskelet);

        if(iskeletAdet>=6)
        {
            GameManager.instance.oyunCikisEkraniniAc();
            StartCoroutine(OyundanCik());
        }

    }

    IEnumerator OyundanCik()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.oyunCikisEkraniniAc();
    }
}
