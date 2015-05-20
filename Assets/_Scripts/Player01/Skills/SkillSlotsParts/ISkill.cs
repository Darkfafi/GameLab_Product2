using UnityEngine;
using System.Collections;

public interface ISkill {
	void ActivateSkill(GameObject target = null, GameObject caster = null);
}
