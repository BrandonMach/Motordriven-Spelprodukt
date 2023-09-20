using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Animate(Vector3 moveDirection)
    {
        animator.SetFloat("VelocityZ", Vector3.Dot(moveDirection, transform.forward), 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", Vector3.Dot(moveDirection, transform.right), 0.1f, Time.deltaTime);

        animator.SetFloat("Lean", Vector3.Dot(moveDirection, transform.right), 0.1f, Time.deltaTime);
    }

    public void Update()
    {
       
    }
}
