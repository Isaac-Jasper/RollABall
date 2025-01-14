using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BallAI : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed;
    public void MoveBall(Vector3 moveDirection) {
        Debug.Log("ballismoving");
        rb.AddForce(moveDirection*Time.deltaTime*moveSpeed);
    }
}
