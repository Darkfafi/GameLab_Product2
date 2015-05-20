using UnityEngine;
using System.Collections;

public class PlayerOne : MonoBehaviour {

	SkillSlots skillSlots;

	// Use this for initialization
	void Awake () {
		gameObject.AddComponent<MoveByKeyboard> ();

		skillSlots = gameObject.AddComponent<SkillSlots> ();
		ISkill homingSkill = new ShootHomingProjectileSkill ();
		skillSlots.AddSlot(homingSkill);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			skillSlots.UseSkillFromSlot(0,GameObject.Find("Player02")); 
		} else if (Input.GetKeyDown (KeyCode.X)) {
			skillSlots.UseSkillFromSlot(1,gameObject.GetComponent<DamageOnTouch>().listOfTargetsToDamage[0]); 
		} else if (Input.GetKeyDown (KeyCode.C)) {
			skillSlots.UseSkillFromSlot(2,gameObject.GetComponent<DamageOnTouch>().listOfTargetsToDamage[0]); 
		}
	}
}
