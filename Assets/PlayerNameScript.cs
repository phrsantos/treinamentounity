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
		playerNameString = "Player " + (gameObject.GetInstanceID() * -1);
		CmdChangeName(playerNameString);
	}

	void UpdateName(string newName){
		playerNameText.text = newName;
	}


	[Command]
    void CmdChangeName(string newName) {
			RpcUpdateName(newName);
    }

	[ClientRpc]
	void RpcUpdateName(string rpcName)
	{
		UpdateName(rpcName);
	}

    public string GetPlayerName()
    {
        return playerNameString;
    }

        // Update is called once per frame
    void Update () {
        if (!isLocalPlayer) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            playerNameString = PlayerPrefs.GetString("Nickname");
            CmdChangeName(playerNameString);
        }
    }



}
