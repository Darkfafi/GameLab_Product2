﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMode : MonoBehaviour {
	public const string TEAMONE = "Slime";
	public const string TEAMTWO = "FireFlies";

	protected NetworkView _networkView;
	protected PlayerOne _playerOne;
	protected GameObject _slime;
	protected List<PlayerTwo> _playerTwoScripts = new List<PlayerTwo>();
	protected List<GameObject> _allFlies = new List<GameObject>();
	protected string _gameModeName;
	protected bool _gameEnded = false;
	protected GameObject _timer;

	protected virtual void Awake () {
		_timer = GameObject.Find ("Timer");
		_timer.GetComponent<Timer> ().TimerEndedEvent += EndTimer;

	}
	protected virtual void Start(){
		_networkView = GetComponent<NetworkView>();
	}
	protected virtual void Update()
	{
	}
	protected virtual void EndTimer () {

	}

	[RPC]
	protected virtual void EndGame(string gameMode, string teamwinner, string winners)
	{
		_timer.GetComponent<Timer> ().PauseTimer ();
		_slime.GetComponent<MoveByKeyboard>().enabled = false;

		foreach(GameObject fly in _allFlies)
		{
			if(fly != null && fly.GetComponent<MoveToTouch>() != null){
				fly.GetComponent<MoveToTouch>().enabled = false;
			}
		}
		GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>().SetEndScreen(gameMode,teamwinner,winners);

		Invoke ("ShowEndScreen", 3f);
	}

	private void ShowEndScreen(){
		GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>().ShowEndscreen();
		_playerOne.GetComponent<MoveableNetworkEntity>().DestroyNetworkObject();
	}

	public virtual void StartGameMode(){
		_timer.GetComponent<Timer> ().StartTimer ();

		_slime = GameObject.FindGameObjectWithTag(Tags.Player1);
		_slime.GetComponent<MoveByKeyboard>().enabled = true;
		_playerOne = _slime.GetComponent<PlayerOne>();
		_playerOne.enabled = true;
		GameObject[] allFlies = GameObject.FindGameObjectsWithTag(Tags.Player2);
		foreach(GameObject fly in allFlies)
		{
			fly.GetComponent<MoveToTouch>().enabled = true;
			_allFlies.Add(fly);
			fly.GetComponent<Lives>().DeathEvent += FlyDied;
			_playerTwoScripts.Add(fly.GetComponent<PlayerTwo>());
		}
	}

	protected virtual void FlyDied(int lives, GameObject fly){

	}
}
