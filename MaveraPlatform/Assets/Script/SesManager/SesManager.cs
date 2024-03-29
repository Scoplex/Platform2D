using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    public static SesManager instance;

    [SerializeField]
    AudioSource[] sesEfektleri;

    private void Awake()
    {
        instance = this;
    }
    public void sesEfektiCikar(int hangiSes)
    {
        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].Play();
    }

    public void KarisikSesEfektiCikar(int hangiSes)
    {
        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].pitch = Random.Range(0.8f, 1.3f);
        sesEfektleri[hangiSes].Play();
    }
}
