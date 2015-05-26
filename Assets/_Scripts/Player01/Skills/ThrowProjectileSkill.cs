using UnityEngine;
using System.Collections;

public class ThrowProjectileSkill : ISkill {
	private float _cooldown = 2;
	private float _currentCooldown = 0;
	public void ActivateSkill(GameObject target, GameObject caster){
		Debug.Log("ThrowSkill");
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
