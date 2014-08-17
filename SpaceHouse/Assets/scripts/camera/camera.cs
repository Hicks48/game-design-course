using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {
	public GameObject focus;

	void Start () {
	
	}
	
	void Update () {
		if (focus != null)
		{
			Vector3 newPosition = new Vector3(focus.transform.position.x, focus.transform.position.y, transform.position.z);
			transform.position = newPosition;
		}
	}
}
