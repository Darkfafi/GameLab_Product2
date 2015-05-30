using UnityEngine;
using System.Collections;

public class DamageOnTouch : MonoBehaviour {

	public GameObject[] listOfTargetsToDamage;
	public int damageToDeal = 1;

	void OnTriggerEnter2D(Collider2D other){
		if(Network.isServer)
		{
			if (ListContainsGameobject(other.gameObject) && other.gameObject.GetComponent<Lives>() != null) {
				other.gameObject.GetComponent<Lives>().SendAddSubLife(-damageToDeal);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(Network.isServer)
		{
			if (ListContainsGameobject(other.gameObject) && other.gameObject.GetComponent<Lives>() != null) {
				other.gameObject.GetComponent<Lives>().SendAddSubLife(-damageToDeal);
			}
		}
	}

	bool ListContainsGameobject(GameObject other){
		bool returnValue = false;
		for (int i = 0; i < listOfTargetsToDamage.Length; i++){
			if(listOfTargetsToDamage[i].tag == other.tag){
				returnValue = true;
				break;
			}
		}
		return returnValue;
	}
}
