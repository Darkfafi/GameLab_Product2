using UnityEngine;
using System.Collections;

public class VectorConverter {
	public static Vector2 GetRotationSyncVector(Vector2 vectorDirection, float rotationInDegrees){

		Vector2[] _vectorList = new Vector2[]{new Vector2(0,Mathf.Abs(vectorDirection.y)),
											  new Vector2(Mathf.Abs(vectorDirection.x),0),
											  new Vector2(0,-Mathf.Abs(vectorDirection.y)),
											  new Vector2(-Mathf.Abs(vectorDirection.x),0)};

		Vector2 calclatedVector = vectorDirection;

		switch(rotationInDegrees.ToString()){
			case "0":
				calclatedVector = vectorDirection;
				break;
			case "90":
				calclatedVector = new Vector2(calclatedVector.y,-calclatedVector.x);
				//calclatedVector = _vectorList[GetArrayLoopIndex(_vectorList,1,System.Array.IndexOf(_vectorList,vectorDirection))];
				break;
			case "180":
				calclatedVector = -vectorDirection;
				break;
			case "270":
				calclatedVector = new Vector2(-calclatedVector.y,calclatedVector.x);
				//calclatedVector = _vectorList[GetArrayLoopIndex(_vectorList,-1,System.Array.IndexOf(_vectorList,vectorDirection))];
				break;
		}
		return calclatedVector;
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
