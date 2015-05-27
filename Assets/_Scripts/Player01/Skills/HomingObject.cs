using UnityEngine;
using System.Collections;

public class HomingObject : MoveableNetworkEntity {

	//timing homing
	private float _timeCreated = float.NaN;
	private float _timeTillStartHoming = 0;

	//homing
	private GameObject _objectToFollow;
	private bool _homing = false;
	private float _mass = 3;
	private Vector2 _directionMoving = new Vector2(0,1);

	protected override void Start ()
	{
		base.Start ();
		_directionMoving = VectorConverter.GetRotationSyncVector (_directionMoving, transform.rotation.eulerAngles.z);
		if(Network.isServer)
			Invoke ("DestroyNetworkObject", 6f);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(_networkView.isMine)
		{
			WhenToFollow();
		}
	}
	private void WhenToFollow()
	{
		if (!float.IsNaN(_timeCreated) && !float.IsNaN (_timeTillStartHoming)){
			if(Time.time > _timeCreated + _timeTillStartHoming){
				_homing = true;
				_timeTillStartHoming = float.NaN;
			}
		}
	}
	public void SetHomingObject(Vector3 startPosition,GameObject objectToFollow,Vector2 startMovingDir,float speed = 2, float timeTillStartHoming = 0){
		syncStartPosition = startPosition;
		_timeCreated = Time.time;
		_objectToFollow = objectToFollow;
		_speed = speed;
		_timeTillStartHoming = timeTillStartHoming;
		_objectSpeed = speed;
		_directionMoving = startMovingDir;
	}

	protected override void MovementInput(){
		if (_homing && _objectToFollow != null) {
			if (_objectSpeed < _speed) {
				_objectSpeed += _speed * 0.015f;
			}
			Vector2 targetPosition = _objectToFollow.transform.position;

			Vector2 desiredStep = targetPosition - new Vector2 (transform.position.x, transform.position.y);

			Vector2 desiredVelocity = desiredStep.normalized * _speed;
		
			Vector2 steeringForce = desiredVelocity - _directionMoving;
			_directionMoving += (steeringForce / _mass);

			//TODO Slow and smooth rotation.
			//transform.eulerAngles = new Vector3 (transform.rotation.x, transform.rotation.y, Mathf.Atan2(desiredStep.y,desiredStep.x) * 180 / Mathf.PI + 90);

		}else {
			if (_objectSpeed > 0) {
				_objectSpeed -= _speed * 0.004f;
			}else if(_objectSpeed <= 0){
				_objectSpeed = 0.5f;
			}
		}
		_rigidBody.transform.position += new Vector3 (_directionMoving.x, _directionMoving.y, 0) * _objectSpeed * Time.deltaTime;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.gameObject.tag);
		if(Network.isServer)
		{
			if(other.gameObject.transform.tag == Tags.Player2)
			{
				other.gameObject.GetComponent<Slime>().SlimePlayer(other.gameObject);
				DestroyNetworkObject();
			}
		}
	}
}
