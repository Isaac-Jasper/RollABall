using Unity.VisualScripting;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    public bool isGrounded;

    public void OnTriggerEnter(Collider col) {
        if (col.tag == "Ground") {
            isGrounded = true;
        }
    }
    public void OnTriggerExit(Collider col) {
        if (col.tag == "Ground") {
            isGrounded = false;
        }
    }
}
