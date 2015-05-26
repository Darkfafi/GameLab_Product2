using UnityEngine;
using System.Collections;

public class ShootHomingProjectileSkill : ISkill {
	private float _cooldown = 10;
	private float _currentCooldown = 0;
	public void ActivateSkill(GameObject target, GameObject caster){
		GameObject missile = Network.Instantiate((GameObject)Resources.Load("Prefabs/Projectiles/HomingMissile"),caster.transform.position,caster.transform.rotation, 0) as GameObject;
		missile.GetComponent<HomingObject> ().SetHomingObject (caster.transform.position,target,1.4f,1f);
	}
	public float cooldown {
		get {
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
