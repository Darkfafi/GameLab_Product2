using UnityEngine;
using System.Collections;

public class Survival : GameMode 
{
	protected virtual void Start()
	{
		base.Start();
		foreach (var fly in _allFlies) {
			fly.GetComponent<Lives>().DeathEvent += CheckFlies;
		}
	}
	private void CheckFlies(int lives) //Win condition player one
	{
		string allWinners = "";
		bool stopGame = true;
		foreach (var flyScript in _playerTwoScripts) {
			if(!flyScript.isDeath)
			{
				stopGame = false;
				break;
			}
		}
		if(stopGame)
		{
			allWinners = _playerOne.usernameText.text;
			EndGame("Surival",GameMode.TEAMONE,allWinners);
		}
	}
	protected override void EndTimer () //Win condition team two
	{
		base.EndTimer ();
		string allWinners = "";
		for (int i = 0; i < _playerTwoScripts.Count; i++) 
		{
			if(i < _playerTwoScripts.Count)
			{
				allWinners += _playerTwoScripts[i].usernameText.text + ",";
			}
			else
			{
				allWinners += _playerTwoScripts[i].usernameText.text;
			}
		}
		EndGame("Surival",GameMode.TEAMTWO,allWinners);
	}
}
