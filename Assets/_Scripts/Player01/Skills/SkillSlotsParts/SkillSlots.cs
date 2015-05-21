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
	public void DeleteSlot(int slotId){
		for (int i = 0; i < _allSlots.Count; i++) {
			if(_allSlots[i].slotId == slotId){
				_allSlots.Remove(_allSlots[i]);
				break;
			}
		}
	}
	public void ChangeSlotSkill(ISkill skill,int slotId){
		for (int i = 0; i < _allSlots.Count; i++) {
			if(_allSlots[i].slotId == slotId){
				_allSlots[i].slotSkill = skill;
				break;
			}
		}
	}
	public void UseSkillFromSlot(int slotId,GameObject target = null){
		for (int i = 0; i < _allSlots.Count; i++) {
			if(_allSlots[i].slotId == slotId){
				_allSlots[i].UseSkill(target,gameObject);
				break;
			}
		}
	}
}
