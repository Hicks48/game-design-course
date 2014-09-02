using UnityEngine;
using System.Collections;

public class MeteoriteShower : MonoBehaviour {

	public GameObject meteoritePrefab;
	public float meteoriteSpawnDistance = 1000f;
	public float meteoriteSpawnSpread = 200f; //width of the meteorite shower
	public float meteoriteShowerLength = 10f; //length of the meteorite shower in seconds
	public int meteoriteCount = 50;

	private Vector3 direction = Vector3.right;
	private Vector3 spawnCenter;
	private Vector3 spawnAxis;
	private float startTime;
	private int spawned = 0;

	// Use this for initialization
	void Start () {
		direction = Common.RotateZ(direction, Random.Range(0, Mathf.PI * 2));
		spawnCenter = -direction * meteoriteSpawnDistance;
		spawnAxis = Common.RotateZ(direction, Mathf.PI / 2);
		startTime = Time.time;
	}

	void Update() {
		float spawnInterval = meteoriteShowerLength / meteoriteCount;
		float timeSinceStart = Time.time - startTime;
		while(spawned < meteoriteCount && (spawned * spawnInterval) < timeSinceStart) {
			SpawnRandomMeteorite();
		}
		if(spawned >= meteoriteCount) Destroy(gameObject);
	}

	private void SpawnRandomMeteorite() {
		Vector3 position = spawnCenter + (spawnAxis * Random.Range(-(meteoriteSpawnSpread / 2), (meteoriteSpawnSpread / 2)));
		GameObject meteoriteInstance = (GameObject) Instantiate(meteoritePrefab, position, Quaternion.identity);
		RayMovement rayMovement = meteoriteInstance.GetComponent<RayMovement>();
		rayMovement.direction = direction;
		spawned++;
	}


}
