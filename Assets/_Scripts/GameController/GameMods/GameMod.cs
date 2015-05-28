using UnityEngine;
using System.Collections;

public class GameMod : MonoBehaviour {

	protected GameObject _timer;

	void Awake () {
		_timer = GameObject.Find ("Timer");
		_timer.GetComponent<Timer> ().TimerEndedEvent += EndTimer;
	}

	void EndTimer () {
		Debug.Log("End Game");
	}
}
