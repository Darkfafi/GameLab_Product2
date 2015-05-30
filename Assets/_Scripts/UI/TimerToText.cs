using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerToText : MonoBehaviour {

	public Timer timer;

	// Use this for initialization
	void Awake () {
		GetComponent<Text> ().text = timer.GetTimeInHumanTimeString ();
		timer.GetComponent<Timer> ().OnTikTimerInfoEvent += TimerTik;
	}
	
	void TimerTik(float timeSec, string timeHumanString){
		GetComponent<Text> ().text = timeHumanString;
	}

}
