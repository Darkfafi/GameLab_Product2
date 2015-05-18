using UnityEngine;
using System.Collections;

public class ConnectionHandler : MonoBehaviour {
	private const string _typeName = "Join Me!";
	private const string _gameName = "Join Me!";
	private string _remoteIP = "172.17.56.249";
	private int _remotePort = 25000;
	private int _maxPlayers = 2;
	private HostData[] _hostList;
	private NetworkView _networkView;
	public GameObject player01Prefab;
	public GameObject player02Prefab;
	public GameObject currentCamera;
	void Awake()
	{
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
	private void StartServer()
	{
		Network.InitializeServer(_maxPlayers, _remotePort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(_typeName, _gameName);
	}
	void OnServerInitialized()
	{
		SpawnPlayer();
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer();
	}
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer)
		{
			Debug.Log("Local server connection disconnected: " + info);
		}
		else if (info == NetworkDisconnection.LostConnection)
		{
			Debug.Log("Lost connection to the server: " + info);
		}
		else
		{
			Debug.Log("Successfully diconnected from the server: " + info);
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
		GameObject player = Network.Instantiate(newPlayer, new Vector3(0f, 5f, 0f), Quaternion.identity, 0) as GameObject;
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
