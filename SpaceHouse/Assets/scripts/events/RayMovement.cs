using UnityEngine;
using System.Collections;

public class RayMovement : MonoBehaviour {
	
	public Vector3 direction = Vector3.right;
	public float speed = 10f;
	public float maxDistance = 1000f;
	public float offset = 0f;

	private bool startRightOfTarget;
	private bool startTopOfTarget;
	private RaycastHit2D hit;
	private Vector3 target;

	void Start () {
		direction = direction.normalized;
		transform.position += (direction * offset);
		hit = Physics2D.Raycast(transform.position, direction, maxDistance);
		if(hit.collider != null) target = hit.point;
		else target = transform.position + (direction * maxDistance);
		startRightOfTarget = target.x < transform.position.x;
		startTopOfTarget = target.y < transform.position.y;
		transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		transform.Rotate(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction * speed * Time.deltaTime;
		if(IsAtTargetLocation()) {
			SendMessage("OnRayMovementHit", hit);
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
