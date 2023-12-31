using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _wasMoving = false;

    public void Animate(Vector3 moveDirection, Vector3 rotateDirection)
    {
        animator.SetFloat("VelocityZ", Vector3.Dot(moveDirection, transform.forward), 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", Vector3.Dot(moveDirection, transform.right), 0.1f, Time.deltaTime);
        animator.SetFloat("Lean", Vector3.Dot(rotateDirection, transform.right), 0.1f, Time.deltaTime);

        bool isMoving = Mathf.Abs(moveDirection.x) >= float.Epsilon && Mathf.Abs(moveDirection.z) >= float.Epsilon;

        if (_wasMoving != isMoving)
        {
            animator.SetBool("isWalking", isMoving);
            _wasMoving = isMoving;
        }
    }
}
