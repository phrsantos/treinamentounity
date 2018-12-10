using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SpeachBalloon : NetworkBehaviour {
    // [SyncVar (hook = "UpdateColor")]
	public Text text;

	[Command]
	void CmdSendText()
	{
		text.text = "Mudei!!";
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
            return;
        }

		if (Input.GetKeyDown(KeyCode.Q)) {
           CmdSendText();
       	}
	}
}
