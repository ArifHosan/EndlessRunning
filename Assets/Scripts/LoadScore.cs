using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour {
	public Text textScore;
	// Use this for initialization
	void Start() {
		int sc = PlayerPrefs.GetInt ("score");
		textScore.text = "Your Score: " + sc;
		int hsc = PlayerPrefs.GetInt ("hiscore");
		if (sc > hsc) {
			PlayerPrefs.SetInt ("hiscore",sc);
		}
	}
}
