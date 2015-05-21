using UnityEngine;
using System.Collections;

public class MoveToTouch : MoveableNetworkEntity {
	
	private Vector2 _destination = Vector2.zero;
	private Vector2 _direction = Vector2.zero;

	private bool _controlling = false;

	protected override void Start ()
	{
		base.Start ();
		_speed = 3;
	}

	protected override void Update ()
	{
		base.Update ();
	}
	private void MovementInput()
	{
		if (Input.GetMouseButton (0)) {
			_destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			_direction = _destination - new Vector2(_rigidBody.transform.position.x, _rigidBody.transform.position.y);
			
			if(_direction.magnitude > _speed / 2 || _objectSpeed <= 0 && _direction.magnitude > 0.6f){
				_objectSpeed = _speed;
			}
		}
		
		if (_objectSpeed > 0) {
			Movement();
			_objectSpeed -= 0.055f; //hoe hard moet het remmen.(des de hoger de value des de sneller dat het object remt).
		}
	}
	private void Movement(){

		_direction.Normalize ();
		_direction *= _objectSpeed;
		_rigidBody.transform.position += new Vector3 (_direction.x, _direction.y, 0) * Time.deltaTime;

	}
}
