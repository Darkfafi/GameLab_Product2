using UnityEngine;
using System.Collections;

public class PlayerOne : MonoBehaviour {

	SkillSlots skillSlots;

	private PlayerGravity _playerGravity;
	// Use this for initialization
	void Awake () {
		MoveByKeyboard newMoveByKeyboard = gameObject.AddComponent<MoveByKeyboard> ();
		GetComponent<NetworkView>().observed = newMoveByKeyboard;

		_playerGravity = GetComponent<PlayerGravity>();

		skillSlots = gameObject.AddComponent<SkillSlots> ();
		ISkill homingSkill = new ShootHomingProjectileSkill ();
		skillSlots.AddSlot(homingSkill);
	}
	
	// Update is called once per frame
	void Update () {
		CheckSkillInput();
	}
	private void CheckSkillInput()
	{
		if (Input.GetKeyDown (KeyCode.Z)) {
			skillSlots.UseSkillFromSlot(0,GameObject.FindGameObjectsWithTag(Tags.Player2)[Random.Range(0,GameObject.FindGameObjectsWithTag(Tags.Player2).Length)]); //homingSkill
		} else if (Input.GetKeyDown (KeyCode.X)) {
			skillSlots.UseSkillFromSlot(1,GameObject.FindGameObjectsWithTag(Tags.Player2)[Random.Range(0,GameObject.FindGameObjectsWithTag(Tags.Player2).Length)]); //Projectile ball
		} else if (Input.GetKeyDown (KeyCode.C)) {
			skillSlots.UseSkillFromSlot(2,gameObject.GetComponent<DamageOnTouch>().listOfTargetsToDamage[0]); //Dash
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.tag == Tags.Surface)
		{
			ChangeRotation(other.transform.eulerAngles.z);
		}
	}
	private void ChangeRotation(float zRotation)
	{
		Vector3 newEulerAngles = this.transform.eulerAngles;
		newEulerAngles.z = zRotation;
		transform.eulerAngles = newEulerAngles;
		_playerGravity.CheckGravity();
	}
}
