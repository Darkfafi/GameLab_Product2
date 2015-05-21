using UnityEngine;
using System.Collections;

public class MoveableNetworkEntity : MonoBehaviour {
	protected float _speed;
	protected float _objectSpeed;
	protected Rigidbody2D _rigidBody;
	protected bool _isGrounded;
	protected NetworkView _networkView;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Quaternion syncStartRotation = Quaternion.identity;
	private Vector3 syncEndPosition = Vector3.zero;
	private Quaternion syncEndRotation = Quaternion.identity;
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
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = _rigidBody.position;
			
			syncEndRotation = syncRotation;
			syncStartRotation = transform.rotation;
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
		syncTime += Time.deltaTime;
		_rigidBody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		
		transform.rotation = Quaternion.Slerp(syncStartRotation, syncEndRotation, syncTime / syncDelay);
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.tag == Tags.Ground)
		{
			_isGrounded = true;
			Debug.Log("Hitting Ground");
		}
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if(other.transform.tag == Tags.Ground)
		{
			_isGrounded = false;
			Debug.Log("Leaving Ground");
		}
	}

}
