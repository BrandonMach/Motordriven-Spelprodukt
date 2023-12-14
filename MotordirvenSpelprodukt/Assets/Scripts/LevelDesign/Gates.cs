using System.Collections;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] float descendDelay = 0.5f;
    [SerializeField] float descendHeight = -2.5f;
    [SerializeField] float descendVelocity = 7.5f;

    void Start() => StartCoroutine(DescendGatesDelay());

    IEnumerator DescendGatesDelay()
    {
        yield return new WaitForSeconds(descendDelay);
        float height = 0;
        while (height > descendHeight)
        {
            float downVelocity = descendVelocity * Time.fixedDeltaTime;
            transform.position = transform.position - new Vector3(0, downVelocity, 0);
            height = transform.localPosition.y;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
