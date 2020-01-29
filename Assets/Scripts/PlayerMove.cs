using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {
	public AudioSource lifeloss, point, gameOver;
	public GameObject[] LifeBar;
	public Text text;
	private int Life;
	private int score;
	public float Speed,moveSpeed;
	// Use this for initialization
	void Start () {
		Life = LifeBar.Length-1;
		score = 0;
		//text.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (Speed, 0, 0);

		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			transform.position+=new Vector3 ( 0, 0, moveSpeed);
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			transform.position+=new Vector3 ( 0, 0, -moveSpeed);
		}

		if (transform.position.z <= -4)
			transform.position = new Vector3 (transform.position.x, transform.position.y, -4f);
		if (transform.position.z >= 4)
			transform.position = new Vector3 (transform.position.x, transform.position.y, 4f);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Enemy") {
			if (Life > 0) {
				LifeBar [Life].SetActive (false);
				Life--;
				lifeloss.Play ();
				col.gameObject.transform.position = new Vector3 (0, 0, 0);
			} else {
				PlayerPrefs.SetInt ("score",score);
				UnityEngine.SceneManagement.SceneManager.LoadScene (2);
			}
		}
		if (col.gameObject.tag == "Rabbit") {
			point.Play ();
			col.gameObject.transform.position = new Vector3(0, 0, 0);
			score += 10;
			text.text="Score: "+score;
		}
	}
}
