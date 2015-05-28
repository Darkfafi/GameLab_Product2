using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {
	private float _strength;
	private float _duration;
	void Start()
	{
		_strength = 2;
		_duration = 2;
	}
	public void SlimePlayer(GameObject player, int stackAmount = 1)
	{
		Player02 playerScript = player.GetComponent<Player02>();
		playerScript.GetSlimed(_strength, _duration, stackAmount);
	}
	/*
	private void SlowObject(MoveableNetworkEntity networkEntityToSlow)
	{
		networkEntityToSlow.SetSpeed(_slowingStrength, _duration);
	}
	private void PullObject(MoveableNetworkEntity networkEntityToPull)
	{
		networkEntityToPull.PullDown(_pullStrength,_duration);
	}
	*/
}
