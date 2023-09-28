using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    public Sprite[] Sprites;

    float fadeDelay = 0.7f;
    float currentAlpha = 1;

    SpriteRenderer _spriteRenderer;

    void Start()
    {
        //int splashSelector = Random.Range(0, 3);
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Sprites[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAlpha -=Time.deltaTime * fadeDelay;

        _spriteRenderer.color = new Color(255, 255, 255, currentAlpha);

        if (currentAlpha <= 0)
        { 
           Destroy(gameObject);
        }      
    }
}
