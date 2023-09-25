using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    public Sprite[] Sprites;
    float timeAlive = 2f;
    float startTime = 0;

    float fadeDelay = 10f;
    float currentAlpha = 1;
    float requiredAlpha = 0;

    [Range(0,1)] public float yo;

    [SerializeField] Color _newColor;

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
        currentAlpha -=Time.deltaTime * 0.7f;

        _spriteRenderer.color = new Color(255, 255, 255, currentAlpha);

        if (currentAlpha <= 0)
        { 
           Destroy(gameObject);
        }      
    }
}
