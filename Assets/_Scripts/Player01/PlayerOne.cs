using UnityEngine;
using System.Collections;

public class PlayerOne : MonoBehaviour {

	SkillSlots skillSlots;

	// Use this for initialization
	void Awake () {
		MoveByKeyboard newMoveByKeyboard = gameObject.AddComponent<MoveByKeyboard> ();
		GetComponent<NetworkView>().observed = newMoveByKeyboard;

		skillSlots = gameObject.AddComponent<SkillSlots> ();
		ISkill homingSkill = new ShootHomingProjectileSkill ();
		skillSlots.AddSlot(homingSkill);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			skillSlots.UseSkillFromSlot(0,GameObject.FindGameObjectsWithTag(Tags.Player2)[Random.Range(0,GameObject.FindGameObjectsWithTag(Tags.Player2).Length)]); //homingSkill
		} else if (Input.GetKeyDown (KeyCode.X)) {
			skillSlots.UseSkillFromSlot(1,GameObject.FindGameObjectsWithTag(Tags.Player2)[Random.Range(0,GameObject.FindGameObjectsWithTag(Tags.Player2).Length)]); //Projectile ball
		} else if (Input.GetKeyDown (KeyCode.C)) {
			skillSlots.UseSkillFromSlot(2,gameObject.GetComponent<DamageOnTouch>().listOfTargetsToDamage[0]); //Dash
		}
	}
}
