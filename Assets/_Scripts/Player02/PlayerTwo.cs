﻿using UnityEngine;
using System.Collections;

public class PlayerTwo : Player {
	private int _shakeCounter = 10;
	private float _shakeTimer = 0f;
	private float _shakeTime = 2.5f;

	private int _slimeStack;
	private float _currentStrength;
	private MoveableNetworkEntity _myMoveScript;

	public bool isDeath = false;
	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		_myMoveScript = GetComponent<MoveableNetworkEntity>();
		GetComponent<Lives>().DeathEvent += Death;
	}
	void OnMouseDown()
	{
		Shake();
	}
	void Death(int lives)
	{
		Destroy(GetComponent<MoveToTouch>());
		isDeath = true;
	}
	public void GetSlimed(float strength, float duration, int stackAmount)
	{
		_slimeStack += stackAmount;
		_currentStrength = strength * (float)_slimeStack * 0.5f;
		_myMoveScript.PullDown(_currentStrength);
		_myMoveScript.AddSpeed(-_currentStrength);

		Invoke("ReduceSlime", duration);
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
			_currentStrength *= (float)_slimeStack * 0.5f;
			_myMoveScript.PullDown(_currentStrength);
			_myMoveScript.AddSpeed(-_currentStrength);
		}
	}

	private void NoLivesLeft(){
		GetComponent<MoveableNetworkEntity> ().DestroyNetworkObject ();
	}
}
