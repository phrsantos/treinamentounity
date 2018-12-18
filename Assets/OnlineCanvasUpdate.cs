using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineCanvasUpdate : MonoBehaviour {

	public Text healthText;
	public Text dashText;
	public Text scoreText;

	private float dashTimer;

	public void UpdateHealthText(int newHealth){
		healthText.text = newHealth.ToString();
	}

	public void UpdateScoreText(int newScore){
		scoreText.text = newScore.ToString();
	}

	public void StartDashTimer(float dashTotalTime){
		dashTimer = dashTotalTime;
	}

	void Update(){
		if (dashTimer > 0){
			dashTimer -= Time.deltaTime;
			dashText.text = dashTimer.ToString();
		}
		else
		{
			dashTimer = 0;
			dashText.text = "Ready!";
		}

	}
}
