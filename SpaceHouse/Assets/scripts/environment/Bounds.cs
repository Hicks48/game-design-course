using UnityEngine;
using System.Collections;

public class Bounds : MonoBehaviour {
	public BoxCollider2D bounds;

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player")) {
			SpaceEngineer engineer = (SpaceEngineer)collider.gameObject.GetComponent<SpaceEngineer>();
			engineer.kill();
			return;
		}

		Destroy (collider.gameObject);
	}
}
