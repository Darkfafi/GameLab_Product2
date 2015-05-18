using UnityEngine;
using System.Collections;

public class MoveableNetworkEntity : MonoBehaviour {
	protected float _speed;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncStartEuler = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Vector3 syncEndEuler = Vector3.zero;
	protected Rigidbody2D _rigidBody;
	private NetworkView _networkView;
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
		Vector3 syncEuler = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = _rigidBody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = _rigidBody.velocity;
			stream.Serialize(ref syncVelocity);
			
			syncEuler = transform.eulerAngles;
			stream.Serialize(ref syncEuler);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			stream.Serialize(ref syncEuler);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = _rigidBody.position;
			
			syncEndEuler = syncEuler * syncDelay;
			syncStartEuler = transform.eulerAngles;
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
	protected void MovementInput()
	{
		
	}
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		_rigidBody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		
		transform.eulerAngles = Vector3.Slerp(syncStartEuler, syncEndEuler, syncTime / syncDelay);
	}


}
