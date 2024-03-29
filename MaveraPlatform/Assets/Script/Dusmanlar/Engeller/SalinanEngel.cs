using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalinanEngel : MonoBehaviour
{
    [SerializeField]
    float donmeHizi = 100f;

    float zAngel;

    [SerializeField]
    float minZAngle = -75f;

    [SerializeField]
    float maxZAngle = 75f;

    private void Start()
    {
        if (Random.Range(0, 2)>0)
        {
            donmeHizi *= -1;
        }
    }

    private void Update()
    {
        zAngel += Time.deltaTime * donmeHizi;
        transform.rotation = Quaternion.AngleAxis(zAngel, Vector3.forward);

        if (zAngel < minZAngle)
        {
            donmeHizi = Mathf.Abs(donmeHizi);
        }
        if (zAngel > donmeHizi)
        {
            donmeHizi = -Mathf.Abs(donmeHizi);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (other.CompareTag("Player") && !other.GetComponent<PlayerHareketKontrol>().playerCanVerdimi)
            {
                other.GetComponent<PlayerHareketKontrol>().GeriTepki();
                other.GetComponent<PlayerCanKontrol>().CaniAzalt();
            }
        }        
    }
}
