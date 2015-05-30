using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMode : MonoBehaviour {
	public const string TEAMONE = "Slime";
	public const string TEAMTWO = "FireFlies";

	protected PlayerOne _playerOne;
	protected GameObject _slime;
	protected List<PlayerTwo> _playerTwoScripts = new List<PlayerTwo>();
	protected List<GameObject> _allFlies = new List<GameObject>();
	protected string _gameModeName;
	protected GameObject _timer;

	protected virtual void Awake () {
		_timer = GameObject.Find ("Timer");
		_timer.GetComponent<Timer> ().TimerEndedEvent += EndTimer;

	}
	protected virtual void Start(){
		_slime = GameObject.FindGameObjectWithTag(Tags.Player1);
		_playerOne = _slime.GetComponent<PlayerOne>();
		GameObject[] allFlies = GameObject.FindGameObjectsWithTag(Tags.Player2);
		foreach(GameObject fly in allFlies)
		{
			_allFlies.Add(fly);
			_playerTwoScripts.Add(fly.GetComponent<PlayerTwo>());
		}
	}
	protected virtual void Update()
	{
	}
	protected virtual void EndTimer () {

	}
	protected virtual void EndGame(string gameMode, string teamwinner, string winners)
	{
		GameObject.Find("GameController").GetComponent<GameController>().ShowEndscreen(gameMode,teamwinner,winners);
	}

	public virtual void StartGameMode(){
		_timer.GetComponent<Timer> ().StartTimer ();
	}
}
