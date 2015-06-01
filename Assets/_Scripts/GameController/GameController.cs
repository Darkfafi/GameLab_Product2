using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject endScreen;
	public Text countDownText;
	private int _counter = 4;

	void CountDown()
	{
		if(_counter != 0)
		{
			_counter--;
			countDownText.text = _counter.ToString();
			Invoke("CountDown", 1f);
		} else {
			countDownText.enabled = false;
			StartGameMode();
		}
	}

	public void BackToMenu(){
		Application.LoadLevel ("Menu");
	}

	public void ShowEndscreen(string gameMod, string winningTeam, string winnerUsername = ""){
		endScreen.SetActive (true);
		endScreen.GetComponent<EndScreenClass> ().ChangeText (winningTeam, winnerUsername);
	}

	public void StartGame(){
		CountDown();
	}
	private void StartGameMode()
	{
		GetComponent<GameMode> ().StartGameMode();
	}
}
