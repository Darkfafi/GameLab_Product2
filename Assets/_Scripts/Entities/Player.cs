using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public string usernameText;
	
	protected NetworkView _networkView;

	private UserInfo _myUserInfo;
	// Use this for initialization
	protected virtual void Awake()
	{
		_networkView = GetComponent<NetworkView>();
	}
	void Start()
	{
		if(_networkView.isMine)
		{
			_myUserInfo = GameObject.FindGameObjectWithTag(Tags.Connector).GetComponent<UserInfo>();
			_networkView.RPC("ShowMyUsername", RPCMode.All, _myUserInfo.username);
			Debug.Log(_networkView.viewID);
		}
	}
	[RPC]
	private void ShowMyUsername(string username)
	{
		usernameText = username;
		this.name = username;
	}
}
