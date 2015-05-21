using UnityEngine;
using System.Collections;

public class MoveByKeyboard : MoveableNetworkEntity {
	private float _jumpForce;
	private int _direction;
	private PlayerGravity _playerGravity;
	protected override void Start ()
	{
		base.Start ();
		_speed = 3;
		_jumpForce = 5;
		_playerGravity = GetComponent<PlayerGravity>();
	}
	
	protected override void Update ()
	{
		base.Update ();
		if(_networkView.isMine)
		{
			Movement();
			_playerGravity.Gravity();
		}
		//CheckGravity();
	}
	
	private void Movement(){
		if (Input.GetKey(KeyCode.D))
		{
			ChangeObjectSpeed(-1);
			_direction = 1;
		}
		if (Input.GetKey(KeyCode.A))
		{
			ChangeObjectSpeed(1);
			_direction = -1;
		}
		if(Input.GetKey(KeyCode.Space) && _isGrounded)
			Jump();
		if(_objectSpeed > 0)
		{
			_objectSpeed -= 0.055f;
		}
		Vector3 movement = new Vector3(VectorConverter.GetRotationSyncVector(new Vector2(1, 0), transform.eulerAngles.z).x,VectorConverter.GetRotationSyncVector(new Vector2(1, 0), transform.eulerAngles.z).y,0);
		_rigidBody.transform.position += movement * _objectSpeed * _direction * Time.deltaTime;
	}
	private void ChangeObjectSpeed(int antiDirection)
	{
		if(_objectSpeed < _speed && _direction != antiDirection)
		{
			_objectSpeed += 0.5f;
		} else
		{
			_objectSpeed = 0;
		}
	}
	private void Jump()
	{
		_rigidBody.velocity = new Vector2(_rigidBody.velocity.x,_jumpForce);
	}
}
