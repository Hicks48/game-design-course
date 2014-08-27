using UnityEngine;
using System.Collections;

public class SpaceEngineer : MonoBehaviour {

	/* Movement variables */
	public Camera mainCamera;
	public float trustFowardPowerMultiplier;
	public float trustBackwardPowerMultiplier;
	public float trustSidewaysPowerMultiplier;
	public float rotationSpeed;
	public float walkingSpeed;

	public KeyCode foward;
	public KeyCode back;

	/* Initial value setters for variables */
	public float initFuel;
	public float initAir;

	/* Space Engineer variables */
	private float fuel;
	private float air;
	private bool alive;
	private bool useOxygen;

	private bool outside;

	void Start () {
		this.fuel = initFuel;
		this.air = initAir;
		this.alive = true;
		this.useOxygen = true;
		this.outside = true;
	}
	
	void Update () {

		/* Check if alive */
		if (!this.alive) {
			return;
		}

		/* Fuel and movement */
		if (this.fuel < 0.0f) {
			this.fuel = 0.0f;

			if(!this.outside) {
				doMovementInside();
			}
		}

		else {
			if (this.outside) {
				if(this.doMovement ()) {
					this.fuel = this.fuel - Time.deltaTime;
				}
			}

			else {
				doMovementInside();
			}
		}

		/* Reduce air */

		if (this.useOxygen) {
			if (this.air > 0.0f) {
				this.air = this.air - Time.deltaTime;
			}

			else {
				this.air = 0.0f;
				this.alive = false;
			}
		}
	}

	void OnGUI() {
		GUI.Box (new Rect(Screen.width - 225, Screen.height - 75, 225, 75), "Life Support");
		GUI.Label (new Rect (Screen.width - 200, Screen.height - 50, 100, 50), "Fuel level:\n" + this.fuel);
		GUI.Label (new Rect (Screen.width - 100, Screen.height - 50, 100, 50), "Air level:\n" + this.air);

		/* Check if palayer is alive */
		if (!this.alive) {
			GUI.Label (new Rect (Screen.width / 2.0f, Screen.height / 2.0f, 100, 50), "Game over\nYou ran out of oxygen");
		}
	}

	public void setOutside(bool isOutside) {
		this.outside = isOutside;
	}

	public void setUseOxygen(bool use) {
		useOxygen = use;
	}

	public float getAir() {
		return this.air;
	}

	public void setAir(float air) {
		this.air = air;
	}

	public void kill() {
		this.alive = false;
	}

	/* Privates */

	private void doMovementInside() {
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0.0f;

		/* Heading = mouse normalized vector to position */
		Vector2 towardsHeading = getToWardsHeading (getHeadingVector ());
		Vector2 foward = this.getFoward ();

		/* Turn towards heading */
		turnTowardsHeading (towardsHeading, foward);

		if (Input.GetKey(this.foward)) {
			rigidbody2D.AddForce(foward * this.walkingSpeed);
		}
		
		if (Input.GetKey (this.back)) {
			rigidbody2D.AddForce((-1) * foward * this.walkingSpeed);
		}
	}

	private bool doMovement() {
		if(this.fuel <= 0.0f) {
			return false;
		}

		/* Movement */
		bool moved = false;
		
		/* Heading = mouse normalized vector to position */
		Vector2 towardsHeading = getToWardsHeading (getHeadingVector ());
		Vector2 foward = this.getFoward ();

		/* Turn towards heading */
		turnTowardsHeading (towardsHeading, foward);
		
		if (Input.GetKey(this.foward)) {
			rigidbody2D.AddForce(foward * trustFowardPowerMultiplier);
			moved = true;
		}
		
		if (Input.GetKey (this.back)) {
			rigidbody2D.AddForce((-1) * towardsHeading * trustBackwardPowerMultiplier);
			moved = true;
		}

		return moved;
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
