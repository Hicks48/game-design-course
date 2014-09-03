using UnityEngine;
using System.Collections;

public class DoorSensorArea : MonoBehaviour {
	public BoxCollider2D sensorArea;
	public Door door;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player")) {
			door.Open();
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player")) {
			door.Close();
		}
	}
}
