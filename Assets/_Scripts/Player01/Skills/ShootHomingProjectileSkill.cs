using UnityEngine;
using System.Collections;

public class ShootHomingProjectileSkill : ISkill {
	
	public void ActivateSkill(GameObject target, GameObject caster){

		GameObject missile = Network.Instantiate((GameObject)Resources.Load("Prefabs/Projectiles/HomingMissile"),caster.transform.position,caster.transform.rotation, 0) as GameObject;
		missile.GetComponent<HomingObject> ().SetHomingObject (target,1.4f,1f);
	}
}
