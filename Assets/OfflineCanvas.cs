using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineCanvas : MonoBehaviour {

	public Text highscoreText;

	void OnEnable () {
		highscoreText.text = PlayerPrefs.GetInt("LocalHighscore", 0).ToString();
	}

}
