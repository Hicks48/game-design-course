using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public GameObject turretShotPrefab;
	public float shotSpeed;

	public Collider2D actionZone;
	public KeyCode actionKey;
	public KeyCode fire;
	public GameObject roof;
	public Camera mainCamera;

	public float rotationSpeed;
	public float coolDown;

	private float sinceLastFire;
	private bool activeControls;

	void Start () {
		this.activeControls = false;
	}
	
	void Update () {
		/* Turret controls */
		if (this.activeControls) {
			/* Heading = mouse normalized vector to position */
			Vector2 towardsHeading = getToWardsHeading (getHeadingVector ());
			Vector2 foward = this.getFoward ();
			
			/* Turn towards heading */
			turnTowardsHeading (towardsHeading, foward);

			if(Input.GetKey(this.fire) && allowedToFire()) {
				Vector3 position = new Vector3(this.transform.position.x + foward.x, this.transform.position.y + foward.y, 0.0f);
				GameObject shot = (GameObject)Instantiate(turretShotPrefab, position, this.transform.rotation);
				shot.rigidbody2D.AddForce(foward * this.shotSpeed);
				this.sinceLastFire = 0.0f;
			}
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player") && Input.GetKey (this.actionKey)) {
			this.activeControls = !this.activeControls;

			/* Do start and end */
			if(this.activeControls) {
				SpaceEngineer engineer = (SpaceEngineer)collider.gameObject.GetComponent<SpaceEngineer>();
				engineer.enabled = false;

				this.roof.SetActive(true);
			}

			else {
				SpaceEngineer engineer = (SpaceEngineer)collider.gameObject.GetComponent<SpaceEngineer>();
				engineer.enabled = true;

				this.roof.SetActive(false);
			}
		}
	}

	private bool allowedToFire() {
		this.sinceLastFire = this.sinceLastFire + Time.deltaTime;

		if (this.sinceLastFire >= this.coolDown) {
			this.sinceLastFire = coolDown;
			return true;
		}

		return false;
	}

	private Vector2 getToWardsHeading(Vector2 heading) {
		/* Normalized vector that points from engineers position to given heading */
		Vector2 foward = new Vector2 (heading.x - transform.position.x, heading.y - transform.position.y);
		return foward.normalized;
	}
	
	private Vector2 getHeadingVector() {
		Vector3 mousePosition3D = mainCamera.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePosition3D.x, mousePosition3D.y);
	}
	
	private Vector2 getFoward() {
		if (this.transform.rotation.eulerAngles.z <= 0.0f && this.transform.rotation.eulerAngles.z >= 90) {
			return new Vector2 (Mathf.Sin (Mathf.Deg2Rad * this.transform.rotation.eulerAngles.z), Mathf.Cos (Mathf.Deg2Rad * this.transform.rotation.eulerAngles.z));
		} 
		
		else if (this.transform.rotation.eulerAngles.z < 90 && this.transform.rotation.eulerAngles.z > 180) {
			return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 90)), Mathf.Sin (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 90)));
		} 
		
		else if (this.transform.rotation.eulerAngles.z < 180 && this.transform.rotation.eulerAngles.z > 270) {
			return new Vector2 (Mathf.Sin (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 180)), Mathf.Cos (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 180)));
		} 
		
		else {
			return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 270)), Mathf.Sin (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 270)));
		}
	}
	
	private void turnTowardsHeading(Vector2 heading, Vector2 foward) {
		float angleHeading = getVectorsUnityAngle (heading);
		float angleFoward = getVectorsUnityAngle (foward);
		
		if (Vector2.Angle (foward, heading) - Mathf.Abs (angleHeading - angleFoward) < 0.1) {
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Lerp (this.transform.rotation.z, Mathf.Rad2Deg * angleHeading, Time.deltaTime * this.rotationSpeed));
		} 
		
		else {
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Lerp (this.transform.rotation.z, Mathf.Rad2Deg * angleHeading, Time.deltaTime * this.rotationSpeed));
		}
	}
	
	private float getVectorsUnityAngle(Vector2 vector) {
		Vector2 j = new Vector2 (0, 1);
		Vector2 v = vector.normalized;
		
		if (v.x < 0) {
			return Mathf.Acos (Vector2.Dot (j, v));
		} 
		
		else {
			return 2 * Mathf.PI - Mathf.Acos (Vector2.Dot (j, v));
		}
	}
}
