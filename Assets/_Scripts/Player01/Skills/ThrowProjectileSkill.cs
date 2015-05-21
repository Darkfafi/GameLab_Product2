using UnityEngine;
using System.Collections;

public class ThrowProjectileSkill : ISkill {

	public void ActivateSkill(GameObject target, GameObject caster){
		Debug.Log("ThrowSkill");
	}
}
