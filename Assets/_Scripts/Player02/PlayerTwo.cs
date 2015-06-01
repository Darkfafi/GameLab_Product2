using UnityEngine;
using System.Collections;

public class PlayerTwo : Player {
	private int _shakeCounter = 5;
	private float _shakeTimer = 0f;
	private float _shakeTime = 1f;

	private int _slimeStack;
	private float _currentStrength;
	private MoveableNetworkEntity _myMoveScript;

	public bool isDeath = false;
	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		_myMoveScript = GetComponent<MoveableNetworkEntity>();
	}
	void OnMouseDown()
	{
		Shake();
	}
	public void GetSlimed(float strength, float duration, int stackAmount)
	{
		_slimeStack += stackAmount;
		_currentStrength = strength * (float)_slimeStack * 0.2f;
		_myMoveScript.PullDown(_currentStrength);
		_myMoveScript.AddSpeed(-_currentStrength);

		Invoke("ReduceSlime", duration);
		Debug.Log (_slimeStack);
	}
	private void Shake()
	{
		_shakeCounter--;
		if(_shakeTimer < Time.time)
		{
			ResetShakeCounter();
		}
		if(_shakeCounter <= 0)
		{
			ResetShakeCounter();
			ReduceSlime();
		}
		_shakeTimer = Time.time + _shakeTime;
		Debug.Log (_shakeCounter);
	}
	private void ResetShakeCounter()
	{
		_shakeCounter = 10;
		_shakeTimer = Time.time + _shakeTime;
	}
	private void ReduceSlime()
	{
		if(_slimeStack != 0)
		{
			_slimeStack--;
			_currentStrength *= (float)_slimeStack * 0.2f;
			_myMoveScript.PullDown(_currentStrength);
			_myMoveScript.AddSpeed(-_currentStrength);
			Invoke("ReduceSlime",2f);
		}
	}

	private void NoLivesLeft(){
		isDeath = true;

	}
	public void RemoveFromGame(){
		_networkView.RPC("DisableMe",RPCMode.All);
	}
	[RPC]
	private void DisableMe()
	{
		gameObject.SetActive(false);
	}
}
