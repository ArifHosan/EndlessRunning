using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGenerate : MonoBehaviour {

	public GameObject[] tilesPrefabs;
	public GameObject[] sideWalkfabs;
	public GameObject[] obstaclefabs;
	public GameObject respawn;

	public int spawnChance;
	public int spawnDistance;

	public int respawnChance, respawnDistance, respawnCount;

	private Transform playerTransform;
	private float spawnZ = 0f;
	private float tileLength = 10.0f;
	private float safeZone=3f;
	private int amnTilesOnScreen = 11;
	private int lastPrefabIndex =0;
	private float lastSpawnX=0,lastRabbitX=0;

	private List<GameObject> activeTiles;
	private List<GameObject> activeSideWalks;
	private List<GameObject> obstacleList;
	private List<GameObject> respawnList;

	// Use this for initialization
	private void Start () {
		activeTiles = new List<GameObject> ();
		activeSideWalks = new List<GameObject> ();
		obstacleList = new List<GameObject> ();
		respawnList = new List<GameObject> ();

		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

		for (int i = 0; i < 8; i++) {
			SpawnTile ();
			SpawnSideWalks ();
		}
	}

	// Update is called once per frame
	private void Update () {
		if (playerTransform.position.x- safeZone > (spawnZ - amnTilesOnScreen-4 * tileLength)) {
			SpawnTile ();
			SpawnSideWalks ();
			SpawnObstacle ();
			SpawnRabbit ();

			DeleteTiles ();
		}
	}

	private void SpawnTile(int prefabIndex = -1){
		GameObject go;
		go = Instantiate (tilesPrefabs [RandomPrefabIndex()]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = new Vector3(1,0,0) * spawnZ;
		spawnZ += tileLength;
		activeTiles.Add (go);
	}

	private void SpawnSideWalks() {
		GameObject go;
		go=Instantiate(sideWalkfabs[Random.Range(0,sideWalkfabs.Length-1)]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = new Vector3 (1, 0, 0) * spawnZ;
		go.transform.position += new Vector3 (0, 0, 6.25f);
		//go.transform.position.z=6.25;
		activeSideWalks.Add (go);

		//GameObject go;
		go=Instantiate(sideWalkfabs[Random.Range(0,sideWalkfabs.Length)]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = new Vector3 (1, 0, 0) * spawnZ;
		go.transform.position += new Vector3 (0, 0, -6.25f);
		//Debug.Log ("x: "+go.transform.position.x);
		//Debug.Log ("y: "+go.transform.position.y);
		//Debug.Log ("z :"+go.transform.position.z);
		//go.transform.position.z=-6.25;
		activeSideWalks.Add (go);
	}

	private void SpawnObstacle() {
		//Debug.Log (playerTransform.position.x);
		if (playerTransform.position.x - lastSpawnX <= spawnDistance)
			return;
		int ran = Random.Range (0, 100);
		//Debug.Log ("Ran "+ran);
		if (ran <= spawnChance) {
			int ri = Random.Range (-4, 4);
			GameObject go;
			go=Instantiate(obstaclefabs[Random.Range(0,obstaclefabs.Length)]) as GameObject;
			go.transform.SetParent (transform);
			go.transform.position = new Vector3 (1, 0, 0) * spawnZ;
			go.transform.position += new Vector3 (0, 0, ri);
			lastSpawnX = go.transform.position.x;
			//Debug.Log ("Hello");
			obstacleList.Add (go);
		}
	}

	private void SpawnRabbit() {
		if (playerTransform.position.x - lastRabbitX <= spawnDistance)
			return;
		int ran = Random.Range (0, 100);
		//Debug.Log ("Ran "+ran);
		if (ran <= respawnChance) {
			int ri = Random.Range (-4, 4);
			GameObject go;
			for(int i=0;i<respawnCount;i++) {
				go=Instantiate(respawn) as GameObject;
				go.transform.SetParent (transform);
				go.transform.position = new Vector3 (1, 0, 0) * spawnZ;
				go.transform.position += new Vector3 (10*i, 1.0f, ri);
				lastRabbitX = go.transform.position.x;
				//Debug.Log ("Hello");
				respawnList.Add(go);
			}
		}
	}

	private void DeleteTiles(){
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);

		Destroy (activeSideWalks [0]);
		Destroy (activeSideWalks [1]);
		activeSideWalks.RemoveAt (0);
		activeSideWalks.RemoveAt (0);

		//if(obstacleList.Count>0) Debug.Log (obstacleList [0].gameObject.transform.position.x + " " + spawnZ);
		while (obstacleList.Count > 0 && obstacleList[0].gameObject.transform.position.x-spawnZ<=-100) {
			Destroy (obstacleList [0]);
			obstacleList.RemoveAt (0);
		}
		while (respawnList.Count > 0 && respawnList[0].gameObject.transform.position.x-lastRabbitX<=-100) {
			Destroy (respawnList [0]);
			respawnList.RemoveAt (0);
		}

	}

	private int RandomPrefabIndex(){
		if (tilesPrefabs.Length <= 1)
			return 0;
		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex) {
			randomIndex = Random.Range (0, tilesPrefabs.Length);
		}
		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
}