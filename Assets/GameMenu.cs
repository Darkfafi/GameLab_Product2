using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMenu : MonoBehaviour {
	private ConnectionHandler _connectionHandler;
	private UserInfo _myUserInfo;
	private bool _pickedUserName = false;
	//private GameObject[] allRooms = new GameObject[10];
	
	public bool inGameRoom;
	public List<string> allUsernames = new List<string>();

	void Awake()
	{
		_myUserInfo = GetComponent<UserInfo>();
		_connectionHandler = GetComponent<ConnectionHandler>();
	}
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if(!_pickedUserName)
			{
				_myUserInfo.username = GUI.TextField(new Rect(Screen.width/2-75,Screen.height/2,150,25), _myUserInfo.username);
				if (GUI.Button(new Rect(Screen.width/2-75, Screen.height/2-110, 150, 50), "Pick Username"))
					_pickedUserName = true;
			} 
			else
			{
				if(_myUserInfo.isTablet == false) //if it is not a tablet you can make a server.
				{
					_connectionHandler.gameName = GUI.TextField(new Rect(Screen.width/2-75,Screen.height/2,150,25), _connectionHandler.gameName);
					if (GUI.Button(new Rect(Screen.width/2-125, Screen.height/2-150, 250, 100), "Start New Server"))
						_connectionHandler.StartServer();
				}
				else //if it is a tablet try to find servers and display them.
				{
					if (GUI.Button(new Rect(Screen.width/2-125, Screen.height/2+100, 250, 100), "Refresh Servers"))
						_connectionHandler.RefreshHostList();
					
					if (_connectionHandler.hostList != null)
					{
						for (int i = 0; i < _connectionHandler.hostList.Length; i++)
						{
							if (GUI.Button(new Rect(Screen.width/2 + 100, Screen.height/2 + (50 * i), 100, 50), _connectionHandler.hostList[i].gameName))
								_connectionHandler.JoinServer(_connectionHandler.hostList[i]);
						}
					} 
				}
			}
		}
		if(inGameRoom)
		{
			for (int i = 0; i < allUsernames.Count; i++) 
			{
				GUI.TextArea(new Rect(Screen.width/2, Screen.height/2+50+50*i, 100, 50), allUsernames[i]);
			}
			if(Network.isServer)
			{
				if(Network.connections.Length > 0)
				{
					if (GUI.Button(new Rect(Screen.width/2, Screen.height/2-100, 250, 100), "Start Game"))
					{
						_connectionHandler.StartGameClicked();
					}
				}
			}
		}
	}
}
