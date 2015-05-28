using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScreenClass : MonoBehaviour {

	public Text winnerText;

	public void ChangeText(string winningTeam, string winningPlayerUserN = ""){
		winnerText.text = "Winning Team: " + winningTeam;
		if (winningPlayerUserN != "") {
			winnerText.text += " Winning player " + winningPlayerUserN;
		}
	}
}
