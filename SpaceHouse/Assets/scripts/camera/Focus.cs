using UnityEngine;
using System.Collections;

public class Focus : MonoBehaviour {
	public GameObject focus;

	void Update () {
		/* Move to focus */
		if (focus != null)
		{
			Vector3 newPosition = new Vector3(focus.transform.position.x, focus.transform.position.y, transform.position.z);
			this.transform.position = newPosition;
		}
	}
}
