using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerNameScript : NetworkBehaviour {

    [SyncVar (hook = "UpdateName")]
	public string playerNameString;

	public Text playerNameText;

    public override void OnStartClient()
    {
		playerNameString = "Player " + Random.Range(1, 10);
		UpdateName(playerNameString);
        CmdChangeName(playerNameString);
	}

	void UpdateName(string newName){
		playerNameText.text = newName;
	}


	[Command]
    void CmdChangeName(string newName) {
            playerNameString = newName;
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
