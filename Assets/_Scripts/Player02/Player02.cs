﻿using UnityEngine;
using System.Collections;

public class Player02 : MonoBehaviour {
	private int _shakeCounter = 10;
	private float _shakeTimer = 0f;
	private float _shakeTime = 2.5f;

	private int _slimeStack;
	private float _currentStrength;
	private MoveableNetworkEntity _myMoveScript;
	// Use this for initialization
	void Awake () {
		_myMoveScript = GetComponent<MoveableNetworkEntity>();
	}

	void OnMouseDown()
	{
		Shake();
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
}