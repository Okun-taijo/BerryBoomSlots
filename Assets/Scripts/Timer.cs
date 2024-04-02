using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _minutes;
    [SerializeField] private int _seconds; 
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private SceneLoader _sceneLoader;

    void Start()
    {
        StartTimerCountdown();
    }

    void StartTimerCountdown()
    {
        int totalSeconds = _minutes * 60 + _seconds;
        StartCoroutine(CountdownTimerRoutine(totalSeconds));
    }

    IEnumerator CountdownTimerRoutine(int totalSeconds)
    {
        while (totalSeconds > 0)
        {
            
            int minutesLeft = totalSeconds / 60;
            int secondsLeft = totalSeconds % 60;

            _timerText.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);

            yield return new WaitForSeconds(1);

            totalSeconds--;
        }

        _timerText.text = "00:00";
        _sceneLoader.GoToMainMenu();
    }
}
