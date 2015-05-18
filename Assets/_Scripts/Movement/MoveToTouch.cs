using UnityEngine;
using System.Collections;

public class MoveToTouch : MoveableNetworkEntity {
	
	private Vector2 _destination = Vector2.zero;
	private float _objectSpeed = 0;

	protected override void Start ()
	{
		base.Start ();
		_speed = 3;
	}

	protected override void Update ()
	{
		base.Update ();

		if (Input.GetMouseButton (0)) {
			_destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			_objectSpeed = _speed;
		}

		if (_objectSpeed > 0) {
			Movement();
			_objectSpeed -= 0.05f; //hoe hard moet het remmen.(des de hoger de value des de sneller dat het object remt).
		}
	}

	private void Movement(){

		Vector2 direction = _destination - new Vector2(transform.position.x, transform.position.y);
		direction.Normalize ();
		direction *= _objectSpeed;
		_rigidBody.transform.position += new Vector3(direction.x,direction.y,0) * Time.deltaTime;
	}
}
