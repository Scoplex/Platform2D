using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkKontrol : MonoBehaviour
{
    [SerializeField]
    GameObject parlamaEfekti;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("iskeletLayer")))
        {
            if (other.CompareTag("iskelet"))
            {
                gameObject.SetActive(false);
                Instantiate(parlamaEfekti, other.transform.position, transform.rotation);
                other.GetComponent<iskeletHealtKontrol>().caniAzalt();
            }
        }
    }
}
