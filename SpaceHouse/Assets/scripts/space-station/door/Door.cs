using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public BoxCollider2D door;
	
	public void Open() {
		this.door.enabled = false;
	}

	public void Close() {
		this.door.enabled = true;
	}
}
