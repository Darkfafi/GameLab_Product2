using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public delegate void NoInfoDelegate();
	public delegate void TimeAllValuesInfo(float fltValue,string stngValue);

	public event NoInfoDelegate TimerStartedEvent;
	public event TimeAllValuesInfo OnTikTimerInfoEvent; 
	public event NoInfoDelegate TimerEndedEvent;

	private bool _timerRunning = false;
	public int timeToCountDownInSeconds = 600;

	private int _currentTime;
	private float _timeTikCounter;

	//Functions to Use

	public void StartTimer(){
		_currentTime = timeToCountDownInSeconds;
		_timerRunning = true;

		if (TimerStartedEvent != null) {
			TimerStartedEvent();
		}
	}

	public void PauseTimer(){
		_timerRunning = false;
	}

	public void ResumeTimer(){
		if (_currentTime > 0) {
			_timerRunning = true;
		}
	}
	
	// Functions in Background

	// Update is called once per frame
	private void Update () {

		if (_timerRunning) {
			_timeTikCounter += Time.deltaTime;
			if(_timeTikCounter >= 1){	
				TimerTik();
			} 
		}
	}
	public string GetTimeInHumanTimeString(){

		float timeInSeconds = _currentTime;

		int minutesCounter = 0;
		int secondsCounter = 0;

		while (timeInSeconds >= 60) {
			timeInSeconds -= 60;
			minutesCounter += 1;
		}
		secondsCounter = (int)timeInSeconds;
		timeInSeconds -= secondsCounter;

		string secString = ":" + secondsCounter.ToString();
		string minString = minutesCounter.ToString();
		

		if(secondsCounter < 10){
			secString = ":0" + secondsCounter.ToString(); 
		}
		if(minutesCounter < 10){
			minString = "0" + minutesCounter.ToString(); 
		}
		
		return minString + secString; 
	}

	private void TimerTik(){

		_timeTikCounter = 1;
		_currentTime -= Mathf.FloorToInt(_timeTikCounter); //countdown
		_timeTikCounter = 0;

		if (_currentTime <= 0) {
			_timerRunning = false;
			_currentTime = 0;

			if(TimerEndedEvent != null){
				TimerEndedEvent();
			}
		}
		if(OnTikTimerInfoEvent != null){
			OnTikTimerInfoEvent(_currentTime,GetTimeInHumanTimeString());
		}
	}
}
