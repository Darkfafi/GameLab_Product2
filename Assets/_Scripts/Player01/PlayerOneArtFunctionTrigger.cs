using UnityEngine;
using System.Collections;

public class PlayerOneArtFunctionTrigger : MonoBehaviour {

	private void JumpForceFunctionActivator(){
		SendMessageUpwards("JumpForceAdd");
	}
	private void LockAnimation(){
		GetComponentInParent<MoveByKeyboard> ()._inLockAnimation = true;
	}
	private void UnlockAnimation(){
		GetComponentInParent<MoveByKeyboard> ()._inLockAnimation = false;
	}
}
