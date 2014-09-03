using UnityEngine;
using System.Collections;

public class TurretShot : MonoBehaviour {
	public float damage;

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag.Equals ("wall")) {
			Strenght str = collider.gameObject.GetComponent<Strenght>();
			str.damage(damage);
		}
		else if(collider.gameObject.tag == "bandit") {
			Destroy(collider.gameObject);
		}

		Destroy (this.gameObject);
	}
}
