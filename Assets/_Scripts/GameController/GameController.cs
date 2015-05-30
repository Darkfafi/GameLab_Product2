using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject endScreen;

	public void BackToMenu(){
		Application.LoadLevel ("Menu");
	}

	public void ShowEndscreen(string gameMod, string winningTeam, string winnerUsername = ""){
		endScreen.SetActive (true);
		endScreen.GetComponent<EndScreenClass> ().ChangeText (winningTeam, winnerUsername);
	}

	public void StartGame(){
		GetComponent<GameMode> ().StartGameMode();
	}
}
