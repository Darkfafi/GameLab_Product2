using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {
	private float _slowingStrength;
	private float _duration;
	void Start()
	{
		_slowingStrength = 2;
		_duration = 2;
	}
	public void SlimePlayer(GameObject player)
	{
		MoveableNetworkEntity playerNetworkEntity = player.GetComponent<MoveableNetworkEntity>();
		SlowObject(playerNetworkEntity);
		PullObject(playerNetworkEntity);
	}
	private void SlowObject(MoveableNetworkEntity networkEntityToSlow)
	{
		networkEntityToSlow.SetSpeed(_slowingStrength, _duration);
	}
	private void PullObject(MoveableNetworkEntity networkEntityToPull)
	{
		networkEntityToPull.PullDown(_duration);
	}
}
