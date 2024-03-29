using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneCikisKontrol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHareketKontrol>().playeriHareketsizYap(); 
            other.GetComponent<PlayerHareketKontrol>().enabled = false;

            FadeKontrol.instance.SeffaftanMataGec();

            StartCoroutine(digerSahneyeGec());
        }
    }

    IEnumerator digerSahneyeGec()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
