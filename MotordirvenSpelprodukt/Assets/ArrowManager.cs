//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ArrowManager : MonoBehaviour
//{
//    [SerializeField] private List<Arrow> arrows = new List<Arrow>();
//    private int currentArrowIndex;


//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }


//    public void FireArrowFromPool(Attack attack, Transform firePos, Vector3 direction)
//    {
//        arrows[currentArrowIndex].FireArrow(attack, firePos, direction);
//        currentArrowIndex = (currentArrowIndex + 1) % arrows.Count;
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private int arrowPoolSize;

    private List<Arrow> arrowPool = new List<Arrow>();
    private int currentArrowIndex;



    void Start()
    {
        for (int i = 0; i < arrowPoolSize; i++)
        {
            Arrow arrow = Instantiate(arrowPrefab);
            arrowPool.Add(arrow);
        }
    }


    /// <summary>
    /// Animation event that gets called by ranged minion when firing arrow.
    /// </summary>
    public void FireArrowFromPool(Attack attack, Transform firePos, Vector3 direction)
    {
        arrowPool[currentArrowIndex].FireArrow(attack, firePos, direction);
        currentArrowIndex = (currentArrowIndex + 1) % arrowPool.Count;
    }
}
