using UnityEngine;
using System.Collections;

public class GameMod : MonoBehaviour {

	protected string _gameModeName;
	protected GameObject _timer;

	protected virtual void Awake () {
		_timer = GameObject.Find ("Timer");
		_timer.GetComponent<Timer> ().TimerEndedEvent += EndTimer;
	}

	protected virtual void EndTimer () {
		Debug.Log("End Game");
		//GameObject.Find("GameController").GetComponent<GameController>().ShowEndscreen(_gameModeName,
	}
}
