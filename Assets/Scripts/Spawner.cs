using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public const int EVENT_CODE_ATTACK = 0;

	public GameObject attackPrefab;

	void Awake()
	{
		// start listening for photon events
		PhotonNetwork.OnEventCall += this.OnEvent;
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			// drop 5 blocks on others when spacebar is pressed
			AttackOtherPlayers (5);
		}

	}

	public void AttackOtherPlayers(int numAttacks)
	{
		// raise event for other players
		PhotonNetwork.RaiseEvent (EVENT_CODE_ATTACK, numAttacks, true, RaiseEventOptions.Default);
	}

	private void OnEvent(byte eventcode, object content, int senderid)
	{
		// determine how to handle this event based on event code
		if (eventcode == EVENT_CODE_ATTACK)
		{
			// parse the content. in this case we know it's an int.
			// we could, for example, pass an array of bytes when raising the event if we needed multiple parameters
			int attacks = (int)content;
			StartCoroutine(SpawnAttacks (attacks));
		}
	}

	// spawns stuff locally
	IEnumerator SpawnAttacks(int numAttacks)
	{
		for (int i = 0; i < numAttacks; i++) 
		{
			// instantiate the prefab and snap it to the spawner's position
			var attackGo = GameObject.Instantiate(attackPrefab);
			attackGo.transform.position = this.transform.position;

			// wait 0.2 seconds before spawning the next block
			yield return new WaitForSeconds (0.2f);
		}

	}

			
}
