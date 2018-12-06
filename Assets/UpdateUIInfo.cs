using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIInfo : MonoBehaviour {

	public Text dashTimerText;

	private float dashTimer;

	// Use this for initialization
	void Start () {

	}

	public void StartDashTimer(float totalTimer)
	{
		dashTimer = totalTimer;
	}

	// Update is called once per frame
	void Update () {

		if(dashTimer <= 0)
		{
			dashTimerText.text = "Ready";
		}else{
			dashTimer -= Time.deltaTime;
			dashTimerText.text = string.Format("Dash: {0}", (int)dashTimer + 1);
		}
	}
}
