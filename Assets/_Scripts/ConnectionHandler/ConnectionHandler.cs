using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConnectionHandler : MonoBehaviour {
	private const string _typeName = "Survival";

	private string _gameName = "Server Name";
	private string _remoteIP = "192.168.1.11";
	private int _remotePort = 25000;
	private int _maxPlayers = 2;
	//private int _maxHosts = 10;
	private NetworkView _networkView;
	private GameMenu _gameMenu;
	private UserInfo _myUserInfo;


	public HostData[] hostList;
	public Text informationText;
	//public GameObject hostButton;
	//public GameObject menuCanvas;
	public GameObject player01Prefab;
	public GameObject player02Prefab;
	public GameObject currentCamera;

	public string gameName{
		set{
			_gameName = value;
		}
		get{
			return _gameName;
		}
	}

	void Awake()
	{
		_networkView = GetComponent<NetworkView>();
		_gameMenu = GetComponent<GameMenu>();
		_myUserInfo = GetComponent<UserInfo>();
	}

	void Start()
	{
		MasterServer.ipAddress = _remoteIP;
		MasterServer.port = 23466;
		Network.natFacilitatorIP = _remoteIP;
		Network.natFacilitatorPort = 50005;
		MasterServer.RequestHostList(_typeName);
	}
	public void StartServer()
	{
		Network.InitializeServer(_maxPlayers, _remotePort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(_typeName, gameName);
	}
	[RPC]
	private void SpawnAllPlayers()
	{
		_gameMenu.inGameRoom = false;
		SpawnPlayer();
		Invoke ("StartGame", 3f);
	}
	private void StartGame()
	{
		GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>().StartGame();
	}
	void OnServerInitialized()
	{
		JoinGameRoom();
	}
	public void StartGameClicked()
	{
		_networkView.RPC("SpawnAllPlayers",RPCMode.All);
		MasterServer.UnregisterHost();
	}

	[RPC]
	public void AddNewUser(string username)
	{
		_gameMenu.allUsernames.Add(username);
	}
	[RPC]
	public void ResetUsernameList()
	{
		_gameMenu.allUsernames.Clear();
	}
	[RPC]
	public void AskAllUsers()
	{
		_networkView.RPC("ResetUsernameList", RPCMode.Others);
		foreach(string username in _gameMenu.allUsernames)
		{
			_networkView.RPC("AddNewUser", RPCMode.Others, username);
		}
	}
	public void JoinGameRoom()
	{
		_gameMenu.inGameRoom = true;
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
		ResetScene();
	}
	void ClearInfoText()
	{
		informationText.text = "";
	}
	void DestroyAllNetworkObjects()
	{
		Network.DestroyPlayerObjects(Network.player);
		if(Network.isServer)
		{
			foreach(NetworkPlayer player in Network.connections)
			{
				Network.DestroyPlayerObjects(player);
			}
		}
	}
	void ResetScene()
	{
		Application.LoadLevel(1);
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		if(Network.connections.Length <= 1 && Network.isServer)
		{
			Network.Disconnect();
			ResetScene();
		} else if(Network.connections.Length <= 0 && Network.isClient)
		{
			ResetScene();
		}
	}
	private void SpawnPlayer()
	{
		GameObject newPlayer = player01Prefab;
		Vector3 spawnPosition = new Vector3(0,-3.6f,0);
		if(Network.isClient) 
		{
			newPlayer = player02Prefab;
			spawnPosition = new Vector3(Random.Range(-5f,5f),3.6f,0);
		}
		Network.Instantiate(newPlayer, spawnPosition, Quaternion.identity, 0);
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
			hostList = MasterServer.PollHostList();
	}
	void OnFailedToConnectToMasterServer(NetworkConnectionError error)
	{
		Debug.Log(error);
	}
	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
}
