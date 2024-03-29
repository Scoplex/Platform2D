using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenerKontrol : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer fenerSprite;

    [SerializeField]
    Sprite fenerOnSprite, fenerOffSprite;

    private void Awake()
    {
        fenerSprite.sprite = fenerOffSprite;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fenerSprite.sprite = fenerOnSprite;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fenerSprite.sprite = fenerOffSprite;
        }
    }
}
