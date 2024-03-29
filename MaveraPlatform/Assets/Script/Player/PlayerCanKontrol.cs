using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanKontrol : MonoBehaviour
{
    public static PlayerCanKontrol instance;
    public int maxSaglik,gecerliSaglik;

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gecerliSaglik = maxSaglik;
        if (UIManager.instance != null)
        {
            UIManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
        }
    }

    public void CaniAzalt()
    {
        UIManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
        gecerliSaglik--;

        if (gecerliSaglik<=0)
        {
            gecerliSaglik = 0;
            PlayerHareketKontrol.instance.PlayerCanVerdi();
        }
    }

    public void CaniArttir()
    {
        gecerliSaglik++;

        if (gecerliSaglik >= maxSaglik)
        {
            gecerliSaglik = maxSaglik;
        }
        UIManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
    }
}
