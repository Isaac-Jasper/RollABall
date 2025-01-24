using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;
public class CollectableScript : MonoBehaviour
{
    [SerializeField]
    UnityEvent onCollect;

    public void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            Deactivate();
            onCollect?.Invoke();
        }
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }
    public void Activate() {
        gameObject.SetActive(true);
    }
}
