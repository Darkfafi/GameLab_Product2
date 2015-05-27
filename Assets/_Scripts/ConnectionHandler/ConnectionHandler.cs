﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConnectionHandler : MonoBehaviour {
	private const string _typeName = "Join Me!";
	private const bool _isTablet = false;

	private string _gameName = "Server Name";
	private string _remoteIP = "172.17.58.198";
	private int _remotePort = 25000;
	private int _maxPlayers = 2;
	//private int _maxHosts = 10;
	private HostData[] _hostList;
	private NetworkView _networkView;
	private UserInfo _myUserInfo;
	private bool _pickedUserName = false;
	private bool _inGameRoom;
	//private GameObject[] allRooms = new GameObject[10];

	private List<string> allUsernames = new List<string>();

	public Text informationText;
	public GameObject hostButton;
	public GameObject menuCanvas;
	public GameObject player01Prefab;
	public GameObject player02Prefab;
	public GameObject currentCamera;

	void Awake()
	{
		_myUserInfo = GetComponent<UserInfo>();
		_networkView = GetComponent<NetworkView>();
	}

	void Start()
	{
		MasterServer.ipAddress = _remoteIP;
		MasterServer.port = 23466;
		Network.natFacilitatorIP = _remoteIP;
		Network.natFacilitatorPort = 50005;
		MasterServer.RequestHostList(_typeName);
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
				if(!_isTablet) //if it is not a tablet you can make a server.
				{
					_gameName = GUI.TextField(new Rect(Screen.width/2-75,Screen.height/2,150,25), _gameName);
					if (GUI.Button(new Rect(Screen.width/2-125, Screen.height/2-150, 250, 100), "Start New Server"))
					{
						StartServer();
					}
				}
				else //if it is a tablet try to find servers and display them.
				{
					if (GUI.Button(new Rect(Screen.width/2-125, Screen.height/2+100, 250, 100), "Refresh Servers"))
					{
						RefreshHostList();
					}
					if (_hostList != null)
					{
						for (int i = 0; i < _hostList.Length; i++)
						{
							if (GUI.Button(new Rect(Screen.width/2 + 100, Screen.height/2 + (50 * i), 100, 50), _hostList[i].gameName))
								JoinServer(_hostList[i]);
						}
					} 
				}
			}
		}
		if(_inGameRoom)
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
						_networkView.RPC("StartGame",RPCMode.All);
						MasterServer.UnregisterHost();
					}
				}
			}
		}
	}
	public void StartServer()
	{
		Network.InitializeServer(_maxPlayers, _remotePort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(_typeName, _gameName);
	}
	[RPC]
	private void StartGame()
	{
		_inGameRoom = false;
		SpawnPlayer();
	}
	void OnServerInitialized()
	{
		JoinGameRoom();
	}

	[RPC]
	public void AddNewUser(string username)
	{
		allUsernames.Add(username);
	}
	[RPC]
	public void ResetUsernameList()
	{
		allUsernames.Clear();
	}
	[RPC]
	public void AskAllUsers()
	{
		_networkView.RPC("ResetUsernameList", RPCMode.Others);
		foreach(string username in allUsernames)
		{
			_networkView.RPC("AddNewUser", RPCMode.All, username);
		}
	}
	public void JoinGameRoom()
	{
		_inGameRoom = true;
		_networkView.RPC("AddNewUser", RPCMode.All,_myUserInfo.username);
		_networkView.RPC("AskAllUsers", RPCMode.Server);
	}
	void OnConnectedToServer()
	{
		JoinGameRoom();
	}
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		string information = "";
		if (Network.isServer)
		{
			information = "Local server connection disconnected: " + info;
		}
		else if (info == NetworkDisconnection.LostConnection)
		{
			information = "Lost connection to the server: " + info;
		}
		else
		{
			information = "Successfully diconnected from the server: " + info;
		}
		DestroyAllNetworkObjects();
		Debug.Log(information);
		informationText.text = information;
		Invoke("ClearInfoText", 1);
	}
	void ClearInfoText()
	{
		informationText.text = "";
	}
	void DestroyAllNetworkObjects()
	{
		Network.DestroyPlayerObjects(Network.player);
		foreach(NetworkPlayer player in Network.connections)
		{
			Network.DestroyPlayerObjects(player);
		}
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		if(Network.isServer && Network.connections.Length <= 0)
		{
			Network.Disconnect();
		}
	}
	private void SpawnPlayer()
	{
		GameObject newPlayer = player01Prefab;
		if(Network.isClient) 
			newPlayer = player02Prefab;
		Network.Instantiate(newPlayer, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	public void RefreshHostList()
	{
		MasterServer.RequestHostList(_typeName);
		/*
		foreach(GameObject room in allRooms)
		{
			Destroy(room);
		}
		if (_hostList != null)
		{
			for (int i = 0; i < _hostList.Length; i++)
			{
				if(i < _maxHosts)
				{
					GameObject newHostButton = Instantiate(hostButton,new Vector3(218f,123f + i*20,0), Quaternion.identity) as GameObject;
					newHostButton.transform.parent = menuCanvas.transform;
					newHostButton.GetComponent<Button>().onClick.AddListener(() => { JoinServer(_hostList[i]);});
					newHostButton.GetComponentInChildren<Text>().text = _hostList[i].gameName;
					allRooms[i] = newHostButton;
				}
			}
		} */
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			_hostList = MasterServer.PollHostList();
	}
	void OnFailedToConnectToMasterServer(NetworkConnectionError error)
	{
		Debug.Log(error);
	}
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
}
