using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadHiScore : MonoBehaviour {
	public Text hiText;
	// Use this for initialization
	void Start () {
		int hs = PlayerPrefs.GetInt ("hiscore");
		hiText.text = "High Score: " + hs;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
