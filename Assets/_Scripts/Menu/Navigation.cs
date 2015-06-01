using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Navigation : MonoBehaviour {

	public GameObject credits;
	public GameObject navText;

	string[] _menuNavList = new string[]{"Start Game","Credits","Exit"};
	int _currentOption = 0;

	void Start(){
		ChangeNavTextOnNavButtonPress(0);
	}

	void ChangeNavTextOnNavButtonPress(int dir){
		int nextOptionIndex = GetArrayLoopIndex (_menuNavList, dir, _currentOption);
		_currentOption = nextOptionIndex;
		navText.GetComponent<Text> ().text = _menuNavList[nextOptionIndex];
	}

	public void PressCurrentOption(){
		switch (_currentOption) {
			case 0:
				Debug.Log("Start Game");
				Application.LoadLevel(1);
			break;
			case 1:
				credits.SetActive(true);
				Debug.Log("Show Credits");
			break;
			case 2:
				Debug.Log("Exit");
				Application.Quit();
			break;
		}
	}

	private static int GetArrayLoopIndex(System.Array array,int amount,int startInArray = 0){
		int returnIndex = startInArray;
		returnIndex = amount + startInArray;
		
		while(returnIndex > array.Length - 1){
			returnIndex -= array.Length;
		}
		while(returnIndex < 0){
			returnIndex += array.Length;
		}
		//Debug.Log (returnIndex);
		return returnIndex;
	}
}
