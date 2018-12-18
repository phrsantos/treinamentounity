using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Score : NetworkBehaviour {

    [SyncVar(hook = "OnChangeScore")]
    private int totalScore = 0;
    private OnlineCanvasUpdate onlineCanvas;

	void Start() {
        onlineCanvas = GameObject.Find("OnlineCanvas").GetComponent<OnlineCanvasUpdate>();
		onlineCanvas.UpdateScoreText(totalScore);

    }

    void OnChangeScore(int score)
    {
        if (isLocalPlayer)
        {
            onlineCanvas.UpdateScoreText(score);
        }
    }

    public override void OnStartLocalPlayer()
    {
        onlineCanvas = GameObject.Find("OnlineCanvas").GetComponent<OnlineCanvasUpdate>();
        onlineCanvas.UpdateScoreText(totalScore);
    }

    public void AddScoreToShooter(int score){
        totalScore += score;

	}

}
