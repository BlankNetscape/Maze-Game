using System.Collections;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private bool isRunning;
    private float elapsedTime;


    // Coroutine-based stopwatch
    private IEnumerator StopwatchCoroutine()
    {
        while (isRunning)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // Starts the stopwatch
    public void StartStopwatch()
    {
        if (!isRunning)
        {
            isRunning = true;
            elapsedTime = 0;
            StartCoroutine(StopwatchCoroutine());
        }
    }

    // Stops the stopwatch
    public void StopStopwatch()
    {
        if (isRunning)
        {
            isRunning = false;
            StopCoroutine(StopwatchCoroutine());
        }
    }

    // Returns the elapsed time in "minutes:seconds" format
    public string GetElapsedTimeString()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
