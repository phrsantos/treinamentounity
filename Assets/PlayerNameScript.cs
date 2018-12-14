using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerNameScript : NetworkBehaviour {

	[SyncVar]
	public string playerNameString;

	public Text playerNameText;

	public override void OnStartClient(){
		UpdateName(playerNameString);
		CmdChangeName(playerNameString);
	}

	public override void OnStartLocalPlayer(){
		playerNameString = "Player " + Random.Range(1, 10);
		CmdChangeName(playerNameString);
	}

	void UpdateName(string newName){
		Debug.Log("UpdateName:" + newName);

		playerNameText.text = newName;
	}


	[Command]
    void CmdChangeName(string newName) {
			Debug.Log("CmdChangeName:" + playerNameString);
			RpcUpdateName(newName);
    }

	[ClientRpc]
	void RpcUpdateName(string rpcName)
	{
		UpdateName(rpcName);
	}

	    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
           CmdChangeName("Mudei o nome");
        }
    }



}
