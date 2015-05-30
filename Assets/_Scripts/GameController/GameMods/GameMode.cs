using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {

	protected string _gameModeName;
	protected GameObject _timer;

	protected virtual void Awake () {
		_timer = GameObject.Find ("Timer");
		_timer.GetComponent<Timer> ().TimerEndedEvent += EndTimer;
	}

	protected virtual void EndTimer () {
		string allWinners = "";
		GameObject[] allFlies = GameObject.FindGameObjectsWithTag(Tags.Player2);
		for (int i = 0; i < allFlies.Length; i++) 
		{
			if(i < allFlies.Length)
			{
				allWinners += allFlies[i].GetComponent<Player>().usernameText.text + ",";
			}
			else
			{
				allWinners += allFlies[i].GetComponent<Player>().usernameText.text;
			}
			Debug.Log(allFlies[i].GetComponent<Player>().usernameText.text);
		}
		GameObject.Find("GameController").GetComponent<GameController>().ShowEndscreen(_gameModeName,"FireFlies",allWinners);
	}

	public virtual void StartGameMode(){
		_timer.GetComponent<Timer> ().StartTimer ();
	}
}
