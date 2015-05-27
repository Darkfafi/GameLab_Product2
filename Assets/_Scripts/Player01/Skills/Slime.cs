using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {
	private float _slowingStrength;
	private float _pullStrength;
	private float _duration;
	private int _slimeStack;
	void Start()
	{
		_slowingStrength = 2;
		_pullStrength = 2;
		_duration = 2;
	}
	public void SlimePlayer(GameObject player)
	{
		_slimeStack++;
		_slowingStrength *= (float)_slimeStack / 0.5f;
		_pullStrength *= (float)_slimeStack / 0.5f;

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
		networkEntityToPull.PullDown(_pullStrength,_duration);
	}
}
