using UnityEngine;
using System.Collections;

public class ShootHomingProjectileSkill : ISkill {
	private float _cooldown = 12;
	private float _currentCooldown = 0;
	public void ActivateSkill(GameObject target, GameObject caster){
		GameObject missile = Network.Instantiate((GameObject)Resources.Load("Prefabs/Projectiles/HomingMissile"),caster.transform.position,Quaternion.identity, 0) as GameObject;
		missile.GetComponent<HomingObject> ().SetHomingObject (caster.transform.position,target,VectorConverter.GetRotationSyncVector(Vector2.up,caster.transform.rotation.eulerAngles.z),1.4f,2f);
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
