using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {

	public GameObject hitPrefab;
	public Vector3 direction;
	public float speed = 10f;

	private bool startRightOfTarget;
	private bool startTopOfTarget;
	private RaycastHit2D hit;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		hit = Physics2D.Raycast(transform.position, direction, 1000f);
		if(hit.collider != null) target = hit.point;
		else target = transform.position + (direction * 1000f);
		startRightOfTarget = target.x < transform.position.x;
		startTopOfTarget = target.y < transform.position.y;
		transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		transform.Rotate(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction * speed * Time.deltaTime;
		if(IsAtTargetLocation()) {
			if(hit.transform != null && hit.transform.tag == "Space Station") Instantiate(hitPrefab, hit.point, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	private bool IsAtTargetLocation() {
		return (startRightOfTarget && transform.position.x < target.x) ||
			   (startTopOfTarget && transform.position.y < target.y) ||
			   (!startRightOfTarget && transform.position.x > target.x) ||
			   (!startTopOfTarget && transform.position.y > target.y);
	}
}
