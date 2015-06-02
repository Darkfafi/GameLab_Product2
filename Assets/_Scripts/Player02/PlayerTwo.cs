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
		if (_slimeStack < 4) {
			_slimeStack += stackAmount;
			_currentStrength = strength * (float)_slimeStack * 0.2f;
			_myMoveScript.PullDown (_currentStrength);
			_myMoveScript.AddSpeed (-_currentStrength);


			_networkView.RPC ("ChangeColor", RPCMode.All);
			_networkView.RPC ("SetAnimation", RPCMode.All, "Hit");
			Invoke ("ReduceSlime", duration);
		}
	}
	private void ChangeColor()
	{
		GetComponent<SpriteRenderer> ().color = new Color (1 / (_slimeStack * 0.5f + 1),1, 1 /(_slimeStack * 0.5f + 1));
	}
	private void Shake()
	{
		_shakeCounter--;
		_networkView.RPC("SetAnimation",RPCMode.All,"Shake");
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
	}
	private void ResetShakeCounter()
	{
		_shakeCounter = 5;
		_shakeTimer = Time.time + _shakeTime;
	}
	private void ReduceSlime()
	{
		if(_slimeStack != 0)
		{
			_slimeStack--;
			_currentStrength *= (float)_slimeStack * 0.2f;
			_myMoveScript.PullDown(_currentStrength);
			_myMoveScript.AddSpeed(0.2f);
			_networkView.RPC ("ChangeColor", RPCMode.All);
			Invoke("ReduceSlime",2.5f);
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
