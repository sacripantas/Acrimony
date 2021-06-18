using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerHandler : MonoBehaviour
{
	public static TimerHandler instance;
	private TimeSpan timePlaying;
	public bool isTimerActive;
	private float elapsedTime;

    public float ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
	public void Awake()
	{
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        isTimerActive = false;

        BeginTimer();
    }
	// Start is called before the first frame update
	void Start()
    {
		
    }

    public void SetTimer(float time) {
        elapsedTime = time;

        StartCoroutine(UpdateTimer());
        isTimerActive = true;
    }

	public void BeginTimer()
	{
		isTimerActive = true;
		elapsedTime = 0f;

		StartCoroutine(UpdateTimer());
	}

	public void StopTimer()
	{
		isTimerActive = false;
	}

	public void ResumeTimer()
	{
		isTimerActive = true;
		StartCoroutine(UpdateTimer());
	}

	IEnumerator UpdateTimer()
	{
		while(isTimerActive)
		{
			elapsedTime += Time.deltaTime;
			timePlaying = TimeSpan.FromSeconds(elapsedTime);
			yield return null;
		}
	}
}
