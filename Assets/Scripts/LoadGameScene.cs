using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameScene : MonoBehaviour {
	public int SceneIndex;
	// Use this for initialization
	public void LoadScene() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (SceneIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
