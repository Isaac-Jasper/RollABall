using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    PlatformMorpher pm;
    private int currentRound;

    public void NextRound() {
        currentRound++;
        if (currentRound <= 2) {
            pm.MorphToLayout(currentRound);
        } else {
            pm.MorphToRandomLayout();
        }
    }
}
