using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

	public delegate void IntLifeValueDelegate(int lives);
	public event IntLifeValueDelegate LostLifeEvent;
	public event IntLifeValueDelegate DeathEvent;
	public event IntLifeValueDelegate AddedLifeEvent;

	public int lives = 3;

	private bool adjustAble = true;

	private NetworkView _networkView;

	public float secondsCooldownAfterHit = 1f;

	private float secondsPassed = 0f;

	void Awake()
	{
		_networkView = GetComponent<NetworkView>();
	}

	[RPC]
	private void AddSubLife(int amount){
		if(adjustAble){
			lives += amount;
			if(amount < 0){
				SendMessage("LostLife",SendMessageOptions.DontRequireReceiver);
				if(LostLifeEvent != null){
					LostLifeEvent(lives);
				}
				if(lives <= 0){
					SendMessage("NoLivesLeft",SendMessageOptions.DontRequireReceiver);
					if(DeathEvent != null){
						DeathEvent(lives);
					}
				}
				HitLessCountdownStart();
			}else if(amount > 0){
				SendMessage("AddedLife",SendMessageOptions.DontRequireReceiver);
				if(AddedLifeEvent != null){
					AddedLifeEvent(lives);
				}
			}
		}
	}
	
	public void SendAddSubLife(int amount)
	{
		_networkView.RPC("AddSubLife", RPCMode.All, amount);
	}
	void HitLessCountdownStart(){
		adjustAble = false;
		SendMessage ("LifeCooldownStarted", SendMessageOptions.DontRequireReceiver);
	}

	void Update(){
		if(!adjustAble){
			secondsPassed += Time.deltaTime;
			if(secondsPassed > secondsCooldownAfterHit){
				adjustAble = true;
				secondsPassed = 0f;
				SendMessage("LifeCooldownEnded",SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
