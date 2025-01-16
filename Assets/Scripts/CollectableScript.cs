using Unity.VisualScripting;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    [SerializeField]
    private ScoreHandler sh;

    public void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            sh.IncreaseScore();
            gameObject.SetActive(false);
        }
    }
}
