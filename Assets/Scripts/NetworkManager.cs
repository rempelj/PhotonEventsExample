using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool connected { get; private set; }

	void Start () 
	{
		// connect to game
		Debug.Log ("Connecting...");
		PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

	// Called when PhotonNetwork.ConnectUsingSettings completes. requires AutoJoinLobby to be true in the PhotonServerSettings
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinOrCreateRoom ("PhotonEventsExample", new RoomOptions (), TypedLobby.Default);
	}
		
	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");
		connected = true;
	}

}
