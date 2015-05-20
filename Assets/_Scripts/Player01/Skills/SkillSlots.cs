using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSlots : MonoBehaviour {

	List<Slot> _allSlots = new List<Slot>();

	public void AddSlot(ISkill skill){
		Slot skillSlot = new Slot ();
		skillSlot.slotId = _allSlots.Count; //dus eerste skill id == 0.
		skillSlot.slotSkill = skill;
		_allSlots.Add (skillSlot);
	}

	public void ChangeSlotSkill(ISkill skill,int slotId){
		for (int i = 0; i < _allSlots.Count; i++) {
			if(_allSlots[i].slotId == slotId){
				_allSlots[i].slotSkill = skill;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			if(_allSlots[0] != null){
				_allSlots[0].UseSkill();
			}
		} else if (Input.GetKeyDown (KeyCode.X)) {
			if(_allSlots[1] != null){
				_allSlots[1].UseSkill();
			}
		} else if (Input.GetKeyDown (KeyCode.C)) {
			if(_allSlots[2] != null){
				_allSlots[2].UseSkill();
			}
		}
	}
}
