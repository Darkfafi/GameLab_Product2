using UnityEngine;
using System.Collections;

public class MoveByKeyboard : MoveableNetworkEntity {
	protected override void Start ()
	{
		base.Start ();
		_speed = 3;
	}
	
	protected override void Update ()
	{
		base.Update ();
		

		if (_objectSpeed > 0) {
			Movement();
			_objectSpeed -= 0.05f; //hoe hard moet het remmen.
		}
	}
	
	private void Movement(){
		
	}
}
