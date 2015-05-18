using UnityEngine;
using System.Collections;

public class MoveByKeyboard : MoveableNetworkEntity {
	private float _jumpForce;
	private float _gravity;
	private Vector2 _currentGravity;
	protected override void Start ()
	{
		base.Start ();
		_speed = 3;
		_jumpForce = 5;
		_gravity = 0.2f;
		_currentGravity = new Vector2(0,-_gravity);
	}
	
	protected override void Update ()
	{
		base.Update ();
		Movement();
		Gravity();
		//CheckGravity();
	}
	
	private void Movement(){
		if (Input.GetKey(KeyCode.D) && _rigidBody.velocity.x < 1)
			_rigidBody.velocity += new Vector2(transform.right.x, 0) * _speed;
		if (Input.GetKey(KeyCode.A) && _rigidBody.velocity.x > -1)
			_rigidBody.velocity += new Vector2(-transform.right.x, 0) * _speed;
		if(Input.GetKey(KeyCode.Space) && _isGrounded)
			Jump();
	}
	private void Gravity()
	{
		_rigidBody.velocity += _currentGravity;
	}
	private void CheckGravity()
	{
		switch (Mathf.FloorToInt(transform.rotation.z)) {
		case 90:
			_currentGravity = new Vector2(_gravity,0);
			break;
		case 180:
			_currentGravity = new Vector2(-_gravity,0);
			break;
		case 270:
			_currentGravity = new Vector2(0,_gravity);
			break;
		default:
			_currentGravity = new Vector2(0,-_gravity);
			break;
		}
	}
	private void Jump()
	{
		_rigidBody.velocity = new Vector2(_rigidBody.velocity.x,_jumpForce);
	}
}
