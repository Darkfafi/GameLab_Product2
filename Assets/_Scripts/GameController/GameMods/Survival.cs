using UnityEngine;
using System.Collections;

public class Survival : GameMode 
{
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
					allWinners += _playerTwoScripts[i].usernameText + ",";
				}
				else
				{
					allWinners += _playerTwoScripts[i].usernameText;
				}
			}
			if(!_gameEnded)
			{
				_networkView.RPC("EndGame",RPCMode.All,"Surival",GameMode.TEAMTWO,allWinners);
				_gameEnded = true;
			}
		}
	}
	protected override void FlyDied (int lives, GameObject fly)
	{
		base.FlyDied (lives, fly);
		if(Network.isServer)
		{
			string allWinners = "";

			_allFlies.Remove(fly);
			if(_allFlies.Count == 0){
				allWinners = _playerOne.usernameText;
				if(!_gameEnded)
				{
					_networkView.RPC("EndGame",RPCMode.All,"Surival",GameMode.TEAMONE,allWinners);
					_gameEnded = true;
				}
			}
			fly.GetComponent<PlayerTwo> ().RemoveFromGame();
		}
	}
}
