using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private List<Arrow> arrows = new List<Arrow>();
    private int currentArrowIndex;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FireArrowFromPool(Attack attack, Transform firePos, Vector3 direction)
    {
        arrows[currentArrowIndex].FireArrow(attack, firePos, direction);
        currentArrowIndex = (currentArrowIndex + 1) % arrows.Count;
    }
}
