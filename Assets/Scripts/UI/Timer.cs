using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public TMP_Text timeText;

    [SerializeField]
    public float currentTime;

    public void Start() {
        currentTime = 0;
    }
    public void Update() {
        currentTime += Time.deltaTime;
        int seconds = (int) currentTime;
        float fractionSeconds = (int) ((currentTime - seconds) * 100);
        timeText.text = "" + seconds;
    }
}
