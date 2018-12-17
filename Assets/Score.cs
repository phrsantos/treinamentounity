using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Score : NetworkBehaviour {

	private int totalScore = 0;
	private string playerName;
    private OnlineCanvasUpdate onlineCanvas;

	void Start() {
		Debug.Log("StartClient");
        onlineCanvas = GameObject.Find("OnlineCanvas").GetComponent<OnlineCanvasUpdate>();
		onlineCanvas.UpdateScoreText(totalScore);
    }

	public void AddScoreToShooter(int score, string shooterName){
		CmdSendHit(score, shooterName);
	}

	[Command]
	void CmdSendHit(int score, string shooterName){
		RpcAddScoreToShooter(score, shooterName);
	}

	[ClientRpc]
	void RpcAddScoreToShooter(int score, string shooterName){
        playerName = gameObject.GetComponent<PlayerNameScript>().GetPlayerName();
		Debug.Log("ClientRpc");
		Debug.Log("shooterName " + shooterName);
		Debug.Log("playerName " + playerName);



        if (isLocalPlayer){
			Debug.Log("Entrou");
			totalScore += score;
            onlineCanvas.UpdateScoreText(totalScore);
        }

    }

}
