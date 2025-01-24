using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int currentScore;
    [SerializeField]
    private TMP_Text scoreText;

    public void Start() {
        currentScore = 0;
    }

    public void IncreaseScore() {
        currentScore++;
        scoreText.text = "Score: " + currentScore;
    }
}
