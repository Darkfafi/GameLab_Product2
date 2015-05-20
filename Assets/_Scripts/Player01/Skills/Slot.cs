using UnityEngine;
using System.Collections;

public class Slot {

	private int _slotId;
	private ISkill _slotSkill;

	public void UseSkill(){
		_slotSkill.ActivateSkill ();
	}

	public ISkill slotSkill{
		set{_slotSkill = value;}
	}
	public int slotId{
		set{_slotId = value;}
		get{return _slotId;}
	}
}
