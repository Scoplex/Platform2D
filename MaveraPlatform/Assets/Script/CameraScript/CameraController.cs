using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float halfYukseksik, halfGenislik;

    Vector2 sonPoz;

    [SerializeField]
    Transform backGrounds;

 

    private void Start()
    {
        halfYukseksik = Camera.main.orthographicSize;
        halfGenislik = halfYukseksik * Camera.main.aspect;

        sonPoz = transform.position;
    }
    private void Update()
    {

        //if (player != null)
        //{
         //   transform.position = new Vector3(
           //    Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik),
             //   Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseksik,boundsBox.bounds.max.y+halfYukseksik),
               // transform.position.z);
        //}

        backGroundHareket();
    }


    void backGroundHareket()
    {
        Vector2 aradakiFark = new Vector2(transform.position.x - sonPoz.x, transform.position.y - sonPoz.y);
        backGrounds.position += new Vector3(aradakiFark.x, aradakiFark.y, 0f);

        sonPoz = transform.position;

    }
}
