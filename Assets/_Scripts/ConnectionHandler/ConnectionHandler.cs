using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectionHandler : MonoBehaviour {
	private const string _typeName = "Join Me!";
	private const string _gameName = "Join Me!";

	private string _remoteIP = "172.17.56.249";
	private int _remotePort = 25000;
	private int _maxPlayers = 2;
	private HostData[] _hostList;
	private NetworkView _networkView;
	private UserInfo _myUserInfo;
	private bool _pickedUserName = false;
	private bool _inGameRoom;

	public Text informationText;
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
				_myUserInfo.username = GUI.TextField(new Rect(Screen.width/2,Screen.height/2,100,50), _myUserInfo.username);
				if (GUI.Button(new Rect(Screen.width/2, Screen.height/2-100, 250, 100), "Play Game"))
					_pickedUserName = true;
			} else
			{
				if (GUI.Button(new Rect(Screen.width/2, Screen.height/2-100, 250, 100), "Start Server"))
					StartServer();
				
				if (GUI.Button(new Rect(Screen.width/2, Screen.height/2+100, 250, 100), "Refresh Hosts"))
					RefreshHostList();
				
				if (_hostList != null)
				{
					for (int i = 0; i < _hostList.Length; i++)
					{
						if (GUI.Button(new Rect(Screen.width/2 + 200, 100 + (110 * i), 100, 50), _hostList[i].gameName))
							JoinServer(_hostList[i]);
					}
				}
			}
		}
		if(Network.isServer && _inGameRoom)
		{
			GUI.TextArea(new Rect(Screen.width/2, Screen.height/2, 100, 50), "Player-1");
			for (int i = 0; i < Network.connections.Length; i++) 
			{
				GUI.TextArea(new Rect(Screen.width/2, Screen.height/2+50+50*i, 100, 50), "Player-" + (i+1).ToString());
			}
			if(Network.connections.Length > 0)
			{
				if (GUI.Button(new Rect(Screen.width/2, Screen.height/2-100, 250, 100), "Start Game"))
				{
					_networkView.RPC("StartGame",RPCMode.All);
				}
			}
		}
		else if(Network.isClient && _inGameRoom)
		{
			GUI.TextArea(new Rect(Screen.width/2, Screen.height/2, 100, 100), "Player-1");
			for (int i = 0; i < Network.connections.Length; i++) 
			{
				GUI.TextArea(new Rect(Screen.width/2, Screen.height/2+50+50*i, 100, 50), "Player-" + (i+1).ToString());
			}
		}
	}
	private void StartServer()
	{
		Network.InitializeServer(_maxPlayers, _remotePort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(_typeName, _gameName);
	}
	[RPC]
	private void StartGame()
	{
		_inGameRoom = false;
		MasterServer.UnregisterHost();
		SpawnPlayer();
	}
	void OnServerInitialized()
	{
		JoinGameRoom();
		//SpawnPlayer();
	}
	void JoinGameRoom()
	{
		_inGameRoom = true;
	}
	void OnConnectedToServer()
	{
		JoinGameRoom();
		//SpawnPlayer();
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
	}
	private void SpawnPlayer()
	{
		GameObject newPlayer = player01Prefab;
		if(Network.isClient) 
			newPlayer = player02Prefab;
		GameObject player = Network.Instantiate(newPlayer, new Vector3(0f, 0f, 0f), Quaternion.identity, 0) as GameObject;
	}
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(_typeName);
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
