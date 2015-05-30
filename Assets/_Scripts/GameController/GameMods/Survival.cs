using UnityEngine;
using System.Collections;

public class Survival : GameMode 
{
	public override void StartGameMode()
	{
		base.StartGameMode();
		foreach (var fly in _allFlies) {
			fly.GetComponent<Lives>().DeathEvent += CheckFlies;
		}
	}
	private void CheckFlies(int lives) //Win condition player one
	{
		Debug.Log("Stuur dit");
		if(Network.isServer)
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
				if(!_gameEnded)
				{
					_networkView.RPC("EndGame",RPCMode.All,"Surival",GameMode.TEAMONE,allWinners);
					_gameEnded = true;
				}
			}
		}
	}
	protected override void EndTimer () //Win condition team two
	{
		base.EndTimer ();
		if(Network.isServer)
		{
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
			if(!_gameEnded)
			{
				_networkView.RPC("EndGame",RPCMode.All,"Surival",GameMode.TEAMTWO,allWinners);
				_gameEnded = true;
			}
		}
	}
}
