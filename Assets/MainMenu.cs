using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Text usernameInputText;
	public GameObject setUsernamePanel;

	private ConnectionHandler _connectionHandler;
	private UserInfo _userInfo;
	// Use this for initialization
	void Awake () {
		GameObject connector = GameObject.FindGameObjectWithTag(Tags.Connector);
		_connectionHandler = connector.GetComponent<ConnectionHandler>();
		_userInfo = connector.GetComponent<UserInfo>();
	}
	
	public void SaveUsername()
	{
		_userInfo.username = usernameInputText.text;
		setUsernamePanel.SetActive(false);
		_connectionHandler.RefreshHostList();
	}
}
