using UnityEngine;
using System.Collections;

public class Slot {

	private int _slotId;
	private ISkill _slotSkill;

	public void UseSkill(GameObject target, GameObject caster = null){
		_slotSkill.ActivateSkill (target,caster);
		//Note : GameObject.Instantiate <-- gebruik voor missile skill!
	}

	public ISkill slotSkill{
		set{_slotSkill = value;}
		get{return _slotSkill;}
	}
	public int slotId{
		set{_slotId = value;}
		get{return _slotId;}
	}
}
