using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    Slider PlayerSlider;

    [SerializeField]
    TMP_Text cointTxt;

    [SerializeField]
    Transform butonlarPanel;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject bitisPanel;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        bitisPanel.SetActive(false);
        pausePanel.SetActive(false);
        butunButonlarinAlphasiniDusur();
        butonlarPanel.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketKontrol.instance.HerseyiKapatNormaliAc();
    }

    public void SlideriGuncelle(int gecerliDeger, int maxDeger)
    {
        PlayerSlider.maxValue = maxDeger;
        PlayerSlider.value = gecerliDeger;
    }

    public void AdetGuncelle()
    {
        cointTxt.text = GameManager.instance.toplananCoinAdet.ToString();
    }

    private void butunButonlarinAlphasiniDusur()
    {
        foreach (Transform btn in butonlarPanel)
        {
            btn.gameObject.GetComponent<CanvasGroup>().alpha = 0.25f;
            btn.GetComponent<Button>().interactable = true;
        }
    }

    public void NormalButonaBasildi()
    {
        butunButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketKontrol.instance.HerseyiKapatNormaliAc();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }
    public void KilicButonaBasildi()
    {
        butunButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketKontrol.instance.HerseyiKapatKiliciAc();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }
    public void OkButonaBasildi()
    {
        butunButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketKontrol.instance.HerseyiKapatOkuAc();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }
    public void MizrakButonaBasildi()
    {
        butunButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketKontrol.instance.herseyiKapatMizrakAc();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }

    public void PausePanelAcKapat()
    {
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("AnaMenu");
    }

    public void BitisPaneliniAc()
    {
        bitisPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OyundanCik()
    {
        Application.Quit();
    }
}
