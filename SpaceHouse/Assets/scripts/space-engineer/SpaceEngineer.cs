using UnityEngine;
using System.Collections;

public class SpaceEngineer : MonoBehaviour {

	/* Movement variables */
	public Camera mainCamera;
	public float trustFowardPowerMultiplier;
	public float trustBackwardPowerMultiplier;
	public float trustSidewaysPowerMultiplier;
	public float rotationSpeed;

	public KeyCode foward;
	public KeyCode back;
	public KeyCode left;
	public KeyCode right;

	/* Initial value setters for variables */
	public float initFuel;
	public float initAir;

	/* Space Engineer variables */
	private float fuel;
	private float air;
	private bool alive;

	void Start () {
		this.fuel = initFuel;
		this.air = initAir;
		this.alive = true;
	}
	
	void Update () {

		/* Check if alive */
		if (!this.alive) {
			return;
		}

		/* Fuel and movement */
		if (this.fuel < 0.0f) {
			this.fuel = 0.0f;
		}

		else {
			if (this.doMovement ()) {
				this.fuel = this.fuel - Time.deltaTime;
			}
		}

		/* Reduce air */
		if (this.air > 0.0f) {
			this.air = this.air - Time.deltaTime;
		}

		else {
			this.air = 0.0f;
			this.alive = false;
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

	private bool doMovement() {
		/* Movement */
		bool moved = false;
		
		/* Heading = mouse normalized vector to position */
		Vector2 towardsHeading = getToWardsHeading (getHeadingVector ());
		Vector2 foward = this.getFoward ();
		Vector2 left = getLeft (foward);
		
		/* Turn towards heading */
		Debug.Log (Mathf.Rad2Deg * Mathf.Asin(this.transform.position.x + towardsHeading.x));
		transform.rotation = Quaternion.Euler (0, 0, Mathf.Lerp(transform.rotation.z, (-1) * Mathf.Rad2Deg * Mathf.Asin(this.transform.position.x + towardsHeading.x), Time.deltaTime * this.rotationSpeed));
		
		if (Input.GetKey(this.foward) && this.fuel > 0.0f) {
			rigidbody2D.AddForce(towardsHeading * trustFowardPowerMultiplier);
			moved = true;
		}
		
		if (Input.GetKey (this.back) && this.fuel > 0.0f) {
			rigidbody2D.AddForce((-1) * towardsHeading * trustBackwardPowerMultiplier);
			moved = true;
		}
		
		if (Input.GetKey (this.left) && this.fuel > 0.0f) {
			rigidbody2D.AddForce(left * trustSidewaysPowerMultiplier);
			moved = true;
		}
		
		if(Input.GetKey (this.right) && this.fuel > 0.0f) {
			rigidbody2D.AddForce((-1) * left * trustSidewaysPowerMultiplier);
			moved = true;
		}

		return moved;
	}

	/* Fix */
	private Vector2 getLeft(Vector2 foward) {
		float xDiff = ((-1) * Mathf.Pow(this.transform.position.x, 2.0f) + foward.x * this.transform.position.x) / ((-1) * this.transform.position.x + foward.x);
		float yDiff = ((-1) * Mathf.Pow(this.transform.position.y, 2.0f) + foward.y * this.transform.position.y) / ((-1) * this.transform.position.y + foward.y);
		return new Vector2(this.transform.position.x - xDiff, this.transform.position.y - yDiff).normalized;
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
		return new Vector2 (Mathf.Sin(this.transform.rotation.z) - this.transform.position.x, Mathf.Cos(this.transform.rotation.z) - this.transform.position.y);
	}
}
