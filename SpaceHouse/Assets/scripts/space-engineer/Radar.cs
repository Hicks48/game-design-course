using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
	public GameObject focus;
	public float maxDistance;

	private CircleCollider2D radarCollider;
	private List<GameObject> gameobjectsInRadious;

	void Start () {
		/* Doesn't work? */
		this.radarCollider = new CircleCollider2D ();

		this.radarCollider.radius = maxDistance;
		this.radarCollider.isTrigger = true;

		this.gameobjectsInRadious = new List<GameObject>();
	}
	
	void Update () {
		if (focus != null) 
		{
			/* Resize radar collider if necesary */
			if(Mathf.Abs(this.radarCollider.radius - this.maxDistance) > 0.00001)
			{
				this.radarCollider.radius = maxDistance;
			}

			/* Move collider to position of focus gameobject */
			Vector3 newPosition = new Vector3(focus.transform.position.x, focus.transform.position.y, transform.position.z);
			transform.position = newPosition;

			/* Clear gameobjects in radious */
			this.gameobjectsInRadious.Clear();
		}
	}

	/* Register gameobjects to draw */
	void OnTriggerStay(Collider other)
	{
		if (other == null || other.gameObject == null) 
		{
			return;
		}

		this.gameobjectsInRadious.Add(other.gameObject);
	}

	void OnGUI()
	{
		GUI.Box(new Rect(0, Screen.height - 50, 50, 50), "Radar");
	}
}
