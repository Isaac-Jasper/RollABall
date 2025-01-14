using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    [SerializeField]
    private float moveTolerance = 0.1f;

    void Update() {
        Vector3 moveDir = Input.GetAxis("Horizontal")*Vector3.right + Input.GetAxis("Vertical")*Vector3.forward;

        if (moveDir.magnitude > moveTolerance) {
            OnMove?.Invoke(moveDir);
        }
    }
}
