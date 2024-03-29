using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletAtakKontrol : MonoBehaviour
{
    [SerializeField]
    Transform atackPoz;

    [SerializeField]
    float atakYariCap;

    [SerializeField]
    LayerMask playerLayer;

    public void AtakYap()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(atackPoz.position, atakYariCap, playerLayer);
        if (playerCollider != null && !playerCollider.GetComponent<PlayerHareketKontrol>().playerCanVerdimi)
        {
            playerCollider.GetComponent<PlayerHareketKontrol>().GeriTepki();
            playerCollider.GetComponent<PlayerCanKontrol>().CaniAzalt();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atackPoz.position, atakYariCap);
    }
}
