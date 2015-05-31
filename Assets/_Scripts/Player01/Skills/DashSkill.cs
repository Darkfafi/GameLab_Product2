using UnityEngine;
using System.Collections;

public class DashSkill : ISkill {

	public float dashForce = 1000;  
	private float _cooldown = 5;
	private float _currentCooldown = 0;
	public void ActivateSkill(GameObject target, GameObject caster){
		Vector2 dashDirection = VectorConverter.GetRotationSyncVector(Vector2.right * -caster.transform.localScale.x,caster.transform.eulerAngles.z);
		caster.GetComponent<Rigidbody2D> ().AddForce (dashDirection * dashForce);
		if (caster.GetComponentInChildren<Animator> () != null) {
			caster.GetComponentInChildren<Animator>().Play("Dash");
		}
	}

	public float cooldown{
		get{
			return _cooldown;
		}
	}
	public float currentCooldown{
		set{
			_currentCooldown = value;
		}
		get{
			return _currentCooldown;
		}
	}
}
