using UnityEngine;
using System.Collections;

public class VectorConverter {
	public static Vector2 GetRotationSyncVector(Vector2 vectorDirection, float rotationInDegrees){

		Vector2 calclatedVector = vectorDirection;

		switch (Mathf.FloorToInt (rotationInDegrees).ToString ()) {
		case "0":
			calclatedVector = vectorDirection;
			break;
		case "90":
			calclatedVector = new Vector2 (-calclatedVector.y, calclatedVector.x);
			break;
		case "180":
			calclatedVector = -vectorDirection;
			break;
		case "270":
			calclatedVector = new Vector2 (calclatedVector.y, -calclatedVector.x);
			break;
		}
		return calclatedVector;
	}
}
