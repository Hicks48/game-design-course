using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	public PolygonCollider2D insideCollider;

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player")) {
			/* Add oxygen to player */
			SpaceEngineer engineer = collider.gameObject.GetComponent<SpaceEngineer>();

			engineer.setOutside(false);

			if(engineer.getAir() != engineer.initAir) {
				engineer.setAir(engineer.initAir);
				engineer.setUseOxygen(false);
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.gameObject.tag.Equals("player")) {
			SpaceEngineer engineer = collider.gameObject.GetComponent<SpaceEngineer>();
			engineer.setUseOxygen(true);
			engineer.setOutside(true);
		}
	}
}
