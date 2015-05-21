using UnityEngine;
using System.Collections;

public class MoveByKeyboard : MoveableNetworkEntity {
	private float _rotationSpeed;
	private float _rotationCooldown;
	private float _currentRotationCooldown;
	private float _jumpForce;
	private int _direction;
	private PlayerGravity _playerGravity;
	private bool _justJumped;
	protected override void Start ()
	{
		base.Start ();
		_speed = 3;
		_jumpForce = 5;
		_rotationSpeed = 0.5f;
		_rotationSpeed = 25;
		_playerGravity = GetComponent<PlayerGravity>();
	}
	
	protected override void Update ()
	{
		base.Update ();
		if(_networkView.isMine)
		{
			_playerGravity.Gravity();
		}
	}
	
	protected override void MovementInput(){
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			ChangeObjectSpeed(-1);
			_direction = 1;
		}

		if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
		{
			ChangeObjectSpeed(1);
			_direction = -1;
		}

		if(Input.GetKey(KeyCode.Space) && _isGrounded)
			Jump();

		if(_objectSpeed > 0)
		{
			_objectSpeed -= 0.085f;
		}

		Vector3 movement = new Vector3(VectorConverter.GetRotationSyncVector(new Vector2(1, 0), transform.eulerAngles.z).x,VectorConverter.GetRotationSyncVector(new Vector2(1, 0), transform.eulerAngles.z).y,0);
		_rigidBody.transform.position += movement * _objectSpeed * _direction * Time.deltaTime;

		if(_justJumped && _currentRotationCooldown >= Time.time)
		{
			Vector3 newEuler = transform.eulerAngles;
			newEuler.z = 0;
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,newEuler, _rotationSpeed * Time.deltaTime);
			if(transform.eulerAngles.z < 0.1f && transform.eulerAngles.z > -0.1f)
			{
				transform.eulerAngles = newEuler;
				_justJumped = false;
				_playerGravity.CheckGravity();
			}
		}
	}
	private void ChangeObjectSpeed(int antiDirection)
	{
		if(_direction != antiDirection)
		{
			if(_objectSpeed < _speed){
				_objectSpeed += 0.5f;
			}
		} else
		{
			_objectSpeed = -_objectSpeed; //ads a bit off slip / feel to it.
		}
		transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x) * -antiDirection, transform.localScale.y, transform.localScale.z);
	}
	private void Jump()
	{
		_rigidBody.velocity = VectorConverter.GetRotationSyncVector(Vector2.up,transform.eulerAngles.z) * _jumpForce;
		_justJumped = true;
		_currentRotationCooldown = _rotationCooldown + Time.time;
	}
}
