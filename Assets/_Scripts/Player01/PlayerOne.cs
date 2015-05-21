using UnityEngine;
using System.Collections;

public class PlayerOne : MonoBehaviour {

	private NetworkView _networkView;

	SkillSlots skillSlots;

	ISkill[] skillList = new ISkill[]{new ShootHomingProjectileSkill(),new DashSkill()};

	// Use this for initialization
	void Awake () {
		MoveByKeyboard newMoveByKeyboard = gameObject.AddComponent<MoveByKeyboard> ();
		_networkView = GetComponent<NetworkView>();
		_networkView.observed = newMoveByKeyboard;

		skillSlots = gameObject.AddComponent<SkillSlots> ();

		ISkill homingSkill = new ShootHomingProjectileSkill ();
		ISkill dashSkill = new DashSkill ();
		skillSlots.AddSlot(homingSkill);
		skillSlots.AddSlot (dashSkill);
	}
	
	// Update is called once per frame
	void Update () {
		if (_networkView.isMine) {
			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.J)) {
				skillSlots.UseSkillFromSlot (0, GameObject.FindGameObjectsWithTag (Tags.Player2) [Random.Range (0, GameObject.FindGameObjectsWithTag (Tags.Player2).Length)]); //homingSkill
			} else if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.K)) {
				skillSlots.UseSkillFromSlot (1, null); //Projectile ball
			} else if (Input.GetKeyDown (KeyCode.C) || Input.GetKeyDown (KeyCode.L)) {
				skillSlots.UseSkillFromSlot (2, gameObject.GetComponent<DamageOnTouch> ().listOfTargetsToDamage [0]); //Dash
			}
		}
	}
}
