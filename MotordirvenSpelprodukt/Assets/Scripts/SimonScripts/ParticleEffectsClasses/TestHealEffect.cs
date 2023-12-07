using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestHealEffect : MonoBehaviour
{
    //[SerializeField] VisualEffect effect1;
    [SerializeField] GameObject effect2;
    [SerializeField] GameObject effect4;
    [SerializeField] GameObject effect5;
    List<GameObject> effects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        effects.Add(Instantiate(effect2, transform.position, Quaternion.Euler(90f, -90f, 0f)));
        effects.Add(Instantiate(effect4, transform.position, Quaternion.Euler(90f, -90f, 0f)));
        effects.Add(Instantiate(effect5, transform.position, Quaternion.Euler(90f, -90f, 0f)));

        foreach (GameObject obj in effects) 
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //effect1.Play();
            //effect2.Play();
            //effect3.Play();
            //effect4.Play();
            //effect5.Play();
            foreach (var item in effects)
            {
                item.transform.parent = Player.Instance.transform;
                item.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (GameObject obj in effects)
            {
                obj.SetActive(false);
            }
        }
    }
}
