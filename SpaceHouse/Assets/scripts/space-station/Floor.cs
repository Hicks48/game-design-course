using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	public PolygonCollider2D insideCollider;
	private bool engineerInside;

	void Start() {
		this.engineerInside = false;
	}

	/* Engineer in side space station */
	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player")) {
			this.engineerInside = true;

			/* Add oxygen to player */
			SpaceEngineer engineer = collider.gameObject.GetComponent<SpaceEngineer>();

			engineer.setOutside(false);
			engineer.airTank.setInUse(false);
			engineer.fuelTank.setInUse(false);

			if(!engineer.airTank.isFull()) {
				engineer.airTank.fill();
			}
		}
	}

	/* Engineer exits spacestation */
	void OnTriggerExit2D(Collider2D collider) {
		if(collider.gameObject.tag.Equals("player")) {
			SpaceEngineer engineer = collider.gameObject.GetComponent<SpaceEngineer>();
			engineer.airTank.setInUse(true);
			engineer.fuelTank.setInUse(true);
			engineer.setOutside(true);
			this.engineerInside = false;
		}
	}

	public bool isEngineerInside() {
		return this.engineerInside;
	}
}
