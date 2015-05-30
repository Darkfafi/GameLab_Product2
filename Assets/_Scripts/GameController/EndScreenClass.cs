using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScreenClass : MonoBehaviour {

	public Text winnerText;

	public void ChangeText(string winningTeam, string winningPlayerUserN = ""){
		winnerText.text = "Winning Team: " + winningTeam + "\n";
		if (winningPlayerUserN != "") {
			winnerText.text += " Winning players: " + winningPlayerUserN;
		}
	}
	void Update()
	{
		if(Input.anyKeyDown)
		{
			Network.Disconnect();
			Application.LoadLevel(0);
		}
		if(Input.GetMouseButtonDown(0))
		{
			Network.Disconnect();
			Application.LoadLevel(0);
		}
	}
}
