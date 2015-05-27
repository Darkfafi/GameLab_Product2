using UnityEngine;
using System.Collections;

public class ThrowProjectileSkill : ISkill {
	private float _cooldown = 2;
	private float _currentCooldown = 0;
	public void ActivateSkill(GameObject target, GameObject caster){
		Debug.Log("ThrowSkill");
		GameObject projectile = Network.Instantiate((GameObject)Resources.Load("Prefabs/Projectiles/BounceBall"),caster.transform.position,caster.transform.rotation,0) as GameObject;
		projectile.GetComponent<BouncingObject> ().SetBounceObject(VectorConverter.GetRotationSyncVector(Vector2.up,caster.transform.rotation.eulerAngles.z),3);
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
