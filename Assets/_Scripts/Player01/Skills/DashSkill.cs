using UnityEngine;
using System.Collections;

public class DashSkill : ISkill {

	public float dashForce = 60;  

	public void ActivateSkill(GameObject target, GameObject caster){
		Vector2 dashDirection = VectorConverter.GetRotationSyncVector(Vector2.right * caster.transform.localScale.x,caster.transform.eulerAngles.z);
		caster.GetComponent<Rigidbody2D> ().AddForce (dashDirection * dashForce);
	}
}
