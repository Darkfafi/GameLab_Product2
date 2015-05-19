using UnityEngine;
using System.Collections;

public class HomingObject : MoveableNetworkEntity {

	//timing homing
	private float _timeCreated;
	private float _timeTillStartHoming = 0;

	//homing
	private GameObject _objectToFollow;
	private bool _homing = false;
	//TODO Also add mass and let the object drag on turn with vectors and mass.

	// Use this for initialization
	void Start () {
		_timeCreated = Time.time;	
	}
	
	// Update is called once per frame
	void Update () {
		if (!float.IsNaN (_timeTillStartHoming)){
			if(Time.time > _timeCreated + _timeTillStartHoming){
				_homing = true;
				_timeTillStartHoming = float.NaN;
			}
		}

		if (_homing) {
			//TODO homing target. Set homing bool true when timeTillStartHoming is past time.
			if(_objectToFollow != null){
				FollowObject();
			}
		}
	}

	void FollowObject(){

	}

	void DestroyHomingObject(){

	}
}
