using UnityEngine;
using System.Collections;

public class BouncingObject : MoveableNetworkEntity {
	
	private Vector2 _movingDirection;
	private int _bounceCounter = 1;

	public void SetBounceObject(Vector2 moveDirection,float speed){
		_movingDirection = moveDirection;
		_speed = speed;
		_objectSpeed = speed;
	}

	protected override void Update ()
	{
		base.Update ();

		_rigidBody.transform.position += new Vector3 (_movingDirection.x, _movingDirection.y,0) * _objectSpeed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == Tags.Surface) {
			_bounceCounter += 1;
			if(_bounceCounter < 6){
				_movingDirection *= -1;
				transform.localScale = new Vector3(transform.localScale.x - 0.01f,(transform.localScale.y - (0.01f * (Mathf.Abs(transform.localScale.y) / transform.localScale.y))) * -1,transform.localScale.z);
			}else{
				if(Network.isServer)
				{
					DestroyNetworkObject();
				}
			}
		}
		if(Network.isServer)
		{
			if(other.transform.tag == Tags.Player2)
			{
				int slimeAmount = Mathf.FloorToInt(2 / (_bounceCounter * 0.5f));
				if(slimeAmount < 1){
					slimeAmount = 1;
				}
				other.gameObject.GetComponent<Slime>().SlimePlayer(other.gameObject,slimeAmount);
				DestroyNetworkObject();
			}
		}
	}
}
