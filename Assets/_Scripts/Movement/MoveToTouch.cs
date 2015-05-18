using UnityEngine;
using System.Collections;

public class MoveToTouch : MonoBehaviour {
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncStartEuler = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Vector3 syncEndEuler = Vector3.zero;
	private Rigidbody _rigidBody;
	private NetworkView _networkView;
	private Animator _animator;

	private void Awake()
	{
		_networkView = this.GetComponent<NetworkView>();
		_rigidBody = this.GetComponent<Rigidbody>();
		//_animator = this.GetComponent<Animator>();
	}
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
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
	void Start () {
	
	}
	
	void Update()
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
	private void MovementInput()
	{

	}
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		_rigidBody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		
		transform.eulerAngles = Vector3.Slerp(syncStartEuler, syncEndEuler, syncTime / syncDelay);
	}
}
