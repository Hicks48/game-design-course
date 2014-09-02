using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public Camera mainCamera;
	public MovementMath movementMath;

	public Collider2D actionZone;
	public KeyCode actionKey;
	public KeyCode fire;

	public float rotationSpeed;
	private bool inControls;

	public GameObject turretShotPrefab;
	public float shotSpeed;

	public float coolDown;
	private float lastFireTime;

	public SpaceEngineer spaceEngineer;

	void Start () {
		this.inControls = false;
	}
	
	void Update () {
		/* Turret controls */
		if (this.inControls) {
			/* Movement */
			/* Heading = mouse normalized vector to position */
			Vector2 towardsHeading = movementMath.getToWardsHeading (movementMath.getHeadingVector (this.mainCamera));
			Vector2 foward = movementMath.getFoward ();
			
			/* Turn towards heading */
			movementMath.turnTowardsHeading (towardsHeading, foward, this.rotationSpeed);

			/* Check fire */
			if(Input.GetKeyDown(this.fire) && allowedToFire()) {
				Vector3 fw = new Vector3(foward.x, foward.y, 0.0f);
				Vector3 position = new Vector3(this.transform.position.x + foward.x, this.transform.position.y + foward.y, 0.0f) + 	(fw * 2.0f);
				GameObject shot = (GameObject)Instantiate(turretShotPrefab, position, this.transform.rotation);
				shot.rigidbody2D.AddForce(foward * this.shotSpeed);
				lastFireTime = Time.time;
			}
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		/* If deactivated */
		if (this.inControls && Input.GetKeyDown (this.actionKey)) {
			this.inControls = false;

			Focus focus = this.mainCamera.gameObject.GetComponent<Focus> ();

			spaceEngineer.enabled = true;
			focus.focus = spaceEngineer.gameObject;
		}

		/* If activated */
		else if (collider.gameObject.tag.Equals ("player") && Input.GetKeyDown (this.actionKey)) {
			this.inControls = true;

			Focus focus = this.mainCamera.gameObject.GetComponent<Focus> ();

			/* Start */
			focus.focus = this.gameObject;
			this.spaceEngineer.enabled = false;
		}
	}

	private bool allowedToFire() {
		return (Time.time - lastFireTime) > coolDown;
	}
}
