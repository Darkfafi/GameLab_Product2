using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	private ConnectionHandler _connectionHandler;
	private UserInfo _userInfo;
	// Use this for initialization
	void Awake () {
		GameObject connector = GameObject.FindGameObjectWithTag(Tags.Connector);
		_connectionHandler = connector.GetComponent<ConnectionHandler>();
		_userInfo = connector.GetComponent<UserInfo>();
	}
	
	public void SaveUsername(string username)
	{
		_userInfo.username = username;
	}
}
