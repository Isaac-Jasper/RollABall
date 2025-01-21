using Unity.VisualScripting;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    public bool isGrounded;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float distance;

    public bool IsGrounded() {
        if (Physics.SphereCast(transform.position, radius, -transform.up, out RaycastHit hit, distance, LayerMask.GetMask("Ground"))) {
            Debug.Log(hit.collider.gameObject.name + "");
            return true;
        }
        return false;
    }

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
