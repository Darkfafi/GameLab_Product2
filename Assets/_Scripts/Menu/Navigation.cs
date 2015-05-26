using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Navigation : MonoBehaviour {

	public GameObject navText;

	string[] _menuNavList = new string[]{"Start Server(Pc only)","Look for server(Tablet only)","Credits","Exit"};
	string[] _chosenOptionList;
	int _currentOption = 0;

	void Start(){
		ChangeNavTextOnNavButtonPress(0);
	}

	public void ChangeNavTextOnNavButtonPress(int dir){
		int nextOptionIndex = GetArrayLoopIndex (_menuNavList, dir, _currentOption);
		_currentOption = nextOptionIndex;
		navText.GetComponent<Text> ().text = _menuNavList[nextOptionIndex];
	}

	private static int GetArrayLoopIndex(System.Array array,int amount,int startInArray = 0){
		int returnIndex = startInArray;
		returnIndex = amount + startInArray;
		
		while(returnIndex > array.Length){
			returnIndex -= array.Length;
		}
		while(returnIndex < 0){
			returnIndex += array.Length;
		}
		//Debug.Log (returnIndex);
		return returnIndex;
	}
}
