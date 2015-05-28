using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerOne : Player {

	SkillSlots skillSlots;

	private PlayerGravity _playerGravity;

	private ISkill[] skillList = new ISkill[]{new ShootHomingProjectileSkill (),new DashSkill (),new ThrowProjectileSkill()};

	// Use this for initialization
	protected override void Awake () 
	{
		base.Awake();
		MoveByKeyboard newMoveByKeyboard = gameObject.AddComponent<MoveByKeyboard> ();
		_networkView.observed = newMoveByKeyboard;

		_playerGravity = GetComponent<PlayerGravity>();

		skillSlots = gameObject.AddComponent<SkillSlots> ();

		for (int i = 0; i < skillList.Length; i++) {
			skillSlots.AddSlot(skillList[i]);
		}
	}
	// Update is called once per frame
	void Update () {
		if (_networkView.isMine) {
			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.J)) {
				skillSlots.UseSkillFromSlot (0, GameObject.FindGameObjectsWithTag (Tags.Player2) [Random.Range (0, GameObject.FindGameObjectsWithTag (Tags.Player2).Length)]); //homingSkill
			} else if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.K)) {
				skillSlots.UseSkillFromSlot (1, null); //Projectile ball
			} else if (Input.GetKeyDown (KeyCode.C) || Input.GetKeyDown (KeyCode.L)) {
				skillSlots.UseSkillFromSlot (2, GameObject.FindGameObjectsWithTag (Tags.Player2) [Random.Range (0, GameObject.FindGameObjectsWithTag (Tags.Player2).Length)]); //Dash
			}
			if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
				Vector3 newLocalScale = usernameText.transform.localScale;
				newLocalScale.x = -0.1f;
				usernameText.transform.localScale = newLocalScale;
			} else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
				Vector3 newLocalScale = usernameText.transform.localScale;
				newLocalScale.x = 0.1f;
				usernameText.transform.localScale = newLocalScale;
			}
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.tag == Tags.Surface)
		{
			SendMessage("HitGround",SendMessageOptions.DontRequireReceiver);
			ChangeRotation(Mathf.FloorToInt(other.transform.eulerAngles.z));
		}
	}
	private void ChangeRotation(float zRotation)
	{
		Vector3 newEulerAngles = this.transform.eulerAngles;
		newEulerAngles.z = zRotation;
		transform.eulerAngles = newEulerAngles;
		_playerGravity.CheckGravity(zRotation);
	}
}
