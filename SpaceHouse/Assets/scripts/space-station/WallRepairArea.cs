using UnityEngine;
using System.Collections;

public class WallRepairArea : MonoBehaviour {
	public BoxCollider2D repairArea;
	public Strenght strenght;
	public KeyCode repairKey;

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("player") && Input.GetKey(this.repairKey)) {
			this.strenght.repair(collider.gameObject);
		}
	}
}
