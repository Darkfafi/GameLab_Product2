using UnityEngine;
using System.Collections;

public class MoveableNetworkEntity : MonoBehaviour {
	protected float _speed;
	protected float _objectSpeed;
	protected Rigidbody2D _rigidBody;
	protected bool _isGrounded;
	protected NetworkView _networkView;

	private float _lastSynchronizationTime = 0f;
	private float _syncDelay = 0f;
	private float _syncTime = 0f;
	private Vector3 _syncStartPosition = Vector3.zero;
	private Quaternion _syncStartRotation = Quaternion.identity;
	private Vector3 _syncEndPosition = Vector3.zero;
	private Quaternion _syncEndRotation = Quaternion.identity;
	private Animator _animator;

	protected virtual void Awake()
	{
		_networkView = this.GetComponent<NetworkView>();
		_rigidBody = this.GetComponent<Rigidbody2D>();
		//_animator = this.GetComponent<Animator>();
	}
	private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		Quaternion syncRotation = Quaternion.identity;;
		if (stream.isWriting)
		{
			syncPosition = _rigidBody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = _rigidBody.velocity;
			stream.Serialize(ref syncVelocity);
			
			syncRotation = transform.rotation;
			stream.Serialize(ref syncRotation);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			stream.Serialize(ref syncRotation);
			
			_syncTime = 0f;
			_syncDelay = Time.time - _lastSynchronizationTime;
			_lastSynchronizationTime = Time.time;
			
			_syncEndPosition = syncPosition + syncVelocity * _syncDelay;
			_syncStartPosition = _rigidBody.position;
			
			_syncEndRotation = syncRotation;
			_syncStartRotation = transform.rotation;
		}
	}
	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	protected virtual void Update()
	{
		if (_networkView.isMine)
		{
			MovementInput();
		}
		else
		{
			SyncedMovement();
		}
	}
	protected virtual void MovementInput()
	{
		
	}
	private void SyncedMovement()
	{
		_syncTime += Time.deltaTime;
		_rigidBody.position = Vector3.Lerp(_syncStartPosition, _syncEndPosition, _syncTime / _syncDelay);
		
		transform.rotation = Quaternion.Slerp(_syncStartRotation, _syncEndRotation, _syncTime / _syncDelay);
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.tag == Tags.Ground)
		{
			_isGrounded = true;
		}
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if(other.transform.tag == Tags.Ground)
		{
			_isGrounded = false;
		}
	}
	public Vector3 syncStartPosition
	{
		get{
			return _syncStartPosition;
		}
		set{
			_syncStartPosition = value;
		}
	}

}
