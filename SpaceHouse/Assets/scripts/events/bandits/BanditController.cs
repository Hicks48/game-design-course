using UnityEngine;
using System.Collections;

public class BanditController : MonoBehaviour {

	public GameObject bulletPrefab;
	public Vector3 centerPosition;
	public float targetDistance = 20f;
	public float spawnDistance = 300f;
	public float rotationSpeed = 10f;
	public float moveInSpeed = 100f;
	public int banditsInCurrentWave = 12;
	public int index = 0;
	public float firingSpeed = 2f;
	public bool lulzMode = false;
	
	private float currentDistance;
	private float spawnTime;
	private bool targetDistanceReached = false;
	private float lastFiringTime;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		spawnDistance += targetDistance;
		currentDistance = spawnDistance;
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		if(targetDistanceReached && IsTimeToFire()) {
			Fire();
		}
	}

	private void Movement() {
		if(!targetDistanceReached) {
			float delta = (Time.time - spawnTime) / ((spawnDistance - targetDistance) / moveInSpeed);
			currentDistance = Mathf.Lerp(spawnDistance, targetDistance, delta);
			if(currentDistance <= targetDistance) OnTargetDistanceReached();
		}
		float lulz = 0f;
		if(lulzMode) {
			lulz = Mathf.Sin(((Mathf.PI * 2) * ((Time.time % rotationSpeed) / rotationSpeed)) + ((Mathf.PI * 2) * ((float) index / banditsInCurrentWave)) * Time.time) * 3f;
		}
		Vector3 newPosition = Vector3.right * (currentDistance + lulz);
		float rotation = ((Mathf.PI * 2) * ((Time.time % rotationSpeed) / rotationSpeed)) + ((Mathf.PI * 2) * ((float) index / banditsInCurrentWave));
		newPosition = Common.RotateZ(newPosition, rotation);
		newPosition += centerPosition;
		transform.position = newPosition;
	}

	private void OnTargetDistanceReached() {
		targetDistanceReached = true;
		lastFiringTime = Time.time + (((float) index / banditsInCurrentWave) * firingSpeed);
	}

	private void Fire() {
		lastFiringTime = Time.time;
		GameObject bulletInstance = (GameObject) Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		RayMovement rayMovement = bulletInstance.GetComponent<RayMovement>();
		rayMovement.direction = (centerPosition - transform.position).normalized;
		rayMovement.maxDistance = currentDistance;
		rayMovement.offset = 0f;
	}

	private bool IsTimeToFire() {
		return Time.time - lastFiringTime >= firingSpeed;
	}
}
