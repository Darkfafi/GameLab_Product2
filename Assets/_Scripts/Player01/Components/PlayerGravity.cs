using UnityEngine;
using System.Collections;

public class PlayerGravity : MonoBehaviour {
	private float _gravity;
	private Vector2 _currentGravity;
	private Rigidbody2D _rigidBody;
	void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
	}
	void Start()
	{
		_gravity = 0.2f;
		_currentGravity = new Vector2(0,-_gravity);
	}
	public void Gravity()
	{
		_rigidBody.velocity += _currentGravity;
	}
	public void CheckGravity()
	{
		switch (Mathf.FloorToInt(transform.eulerAngles.z)) {
		case 90:
			_currentGravity = new Vector2(-_gravity,0);
			break;
		case 180:
			_currentGravity = new Vector2(0,_gravity);
			break;
		case 270:
			_currentGravity = new Vector2(_gravity,0);
			break;
		default:
			_currentGravity = new Vector2(0,-_gravity);
			break;
		}
	}
}
