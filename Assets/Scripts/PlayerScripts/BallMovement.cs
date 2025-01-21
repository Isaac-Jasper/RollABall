using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GroundChecker gc;
    [SerializeField]
    private float moveSpeed, jumpForce;

    public void MoveBall(Vector3 moveDirection) {
        rb.AddForce(moveDirection*Time.deltaTime*moveSpeed);
    }
    public void JumpBall() {
        if (gc.IsGrounded()) {
            rb.AddForce(Vector3.up*jumpForce);
        }
    }
}
