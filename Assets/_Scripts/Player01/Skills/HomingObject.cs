using UnityEngine;
using System.Collections;

public class HomingObject : MoveableNetworkEntity {

	//timing homing
	private float _timeCreated = float.NaN;
	private float _timeTillStartHoming = 0;

	//homing
	private GameObject _objectToFollow;
	private bool _homing = false;
	private float _slowingRadius = 100;
	private float _mass = 3;

	private Vector2 _directionMoving = new Vector2(0,1);
	//TODO Also add mass and let the object drag on turn with vectors and mass.

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (!float.IsNaN(_timeCreated) && !float.IsNaN (_timeTillStartHoming)){
			if(Time.time > _timeCreated + _timeTillStartHoming){
				_homing = true;
				_timeTillStartHoming = float.NaN;
			}
		}

		Move();
	}

	public void SetHomingObject(GameObject objectToFollow,float speed = 2, float timeTillStartHoming = 0){
		_timeCreated = Time.time;
		_objectToFollow = objectToFollow;
		_speed = speed;
		_timeTillStartHoming = timeTillStartHoming;
		_objectSpeed = 1f;
	}

	void Move(){
		if (_homing && _objectToFollow != null) {
			Vector2 targetPosition = _objectToFollow.transform.position;

			Vector2 desiredStep	= targetPosition - new Vector2(transform.position.x,transform.position.y);
			float distanceToTarget = desiredStep.magnitude;

			Vector2 desiredVelocity	= desiredStep * _speed;

			if (distanceToTarget < _slowingRadius){

				desiredVelocity	=	desiredVelocity * (desiredVelocity.magnitude / _slowingRadius);
			}

			Vector2 steeringForce = desiredVelocity - _directionMoving;
			_directionMoving += (steeringForce / _mass);

			transform.rotation = new Quaternion(transform.rotation.x,Vector2.Angle(new Vector2(transform.position.x,transform.position.y),_directionMoving),transform.rotation.z,transform.rotation.w);
		} 
		transform.position += new Vector3 (_directionMoving.x, _directionMoving.y, 0) * _objectSpeed;
	}

	void DestroyHomingObject(){

	}
}
